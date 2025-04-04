import { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { Formik, Form } from "formik";
import { policyValidationSchema } from "../../utils/validators";
import InputField from "../common/FormComponents/InputField";
import SelectField from "../common/FormComponents/SelectField";
import DatePickerField from "../common/FormComponents/DatePicker";
import MultiSelectField from "../common/FormComponents/MultiSelectField";
import policyService from "../../services/policyService";

const PolicyForm = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [loading, setLoading] = useState(id ? true : false);
  const [error, setError] = useState("");
  const [policy, setPolicy] = useState(null);
  const [coverageOptions, setCoverageOptions] = useState([]);
  const [policyTypes, setPolicyTypes] = useState([]);
  const [policyStates, setPolicyStates] = useState([]);
  const isEditMode = !!id;

  // Default initial values for new policy
  const initialValues = {
    numeroPoliza: "",
    tipoPolizaId: "",
    tipoPolizaNombre: "",
    cedulaAsegurado: "",
    montoAsegurado: "",
    fechaVencimiento: null,
    fechaEmision: null,
    coberturasIds: [],
    coberturasNombres: [],
    estadoPolizaId: "",
    estadoPolizaNombre: "",
    prima: "",
    periodo: null,
    fechaInclusion: new Date(),
    aseguradora: "",
  };

  useEffect(() => {
    // Load data
    const loadData = async () => {
      setLoading(true);
      try {
        await Promise.all([
          loadCoverages(),
          loadPolicyTypes(),
          loadPolicyStates(),
        ]);
        // Load policy data if in edit mode
        if (isEditMode) {
          await loadPolicy();
        }
      } catch (err) {
        console.error("Error loading data:", err);
      } finally {
        setLoading(false);
      }
    };
    loadData();
  }, [id]);
  const loadCoverages = async () => {
    try {
      const coverages = await policyService.getCoverages();
      const formattedCoverages = coverages.map((coverage) => ({
        value: coverage.id.toString(),
        label: coverage.nombre,
      }));
      setCoverageOptions(formattedCoverages);
      return formattedCoverages;
    } catch (err) {
      console.error("Error loading coverages:", err);
      setError("Error al cargar las coberturas");
      return [];
    }
  };
  const loadPolicyTypes = async () => {
    try {
      const types = await policyService.getPolicyTypes();
      const formattedTypes = types.map((type) => ({
        value: type.id.toString(),
        label: type.nombre,
      }));
      setPolicyTypes(formattedTypes);
      return formattedTypes;
    } catch (err) {
      console.error("Error loading policy types:", err);
      setError("Error al cargar los tipos de póliza");
      return [];
    }
  };
  const loadPolicyStates = async () => {
    try {
      const states = await policyService.getPolicyStates();
      const formattedStates = states.map((state) => ({
        value: state.id.toString(),
        label: state.nombre,
      }));
      setPolicyStates(formattedStates);
      return formattedStates;
    } catch (err) {
      console.error("Error loading policy states:", err);
      setError("Error al cargar los estados de póliza");
      return [];
    }
  };

  const loadPolicy = async () => {
    try {
      const data = await policyService.getPolicyById(id);
      setPolicy(data);
      return data;
    } catch (err) {
      setError("Error al cargar la póliza");
      console.error(err);
      return null;
    }
  };

  const handleSubmit = async (values, { setSubmitting }) => {
    try {
      setError("");
      // Transform form values to match backend DTO structure
      const policyData = {
        numeroPoliza: values.numeroPoliza,
        tipoPolizaId: parseInt(values.tipoPolizaId),
        tipoPolizaNombre: values.tipoPolizaNombre,
        cedulaAsegurado: values.cedulaAsegurado,
        montoAsegurado: parseFloat(values.montoAsegurado),
        fechaVencimiento: values.fechaVencimiento,
        fechaEmision: values.fechaEmision,
        estadoPolizaId: parseInt(values.estadoPolizaId),
        estadoPolizaNombre: values.estadoPolizaNombre,
        prima: parseFloat(values.prima),
        periodo: values.periodo,
        fechaInclusion: values.fechaInclusion,
        aseguradora: values.aseguradora,
        coberturasIds: values.coberturasIds.map((id) => parseInt(id)),
        coberturasNombres: values.coberturasNombres,
      };
      if (isEditMode) {
        await policyService.updatePolicy(id, policyData);
      } else {
        await policyService.createPolicy(policyData);
      }
      navigate("/policies");
    } catch (err) {
      setError("Error al guardar la póliza");
      console.error(err);
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return (
      <div className="text-center my-5">
        <div className="spinner-border" role="status">
          <span className="visually-hidden">Cargando...</span>
        </div>
      </div>
    );
  }

  return (
    <div>
      <h2>{isEditMode ? "Editar Póliza" : "Nueva Póliza"}</h2>
      {error && (
        <div className="alert alert-danger" role="alert">
          {error}
        </div>
      )}
      <div className="card">
        <div className="card-body">
          <Formik
            initialValues={policy || initialValues}
            onSubmit={handleSubmit}
            enableReinitialize
          >
            {({ isSubmitting }) => (
              <Form>
                <div className="row">
                  <div className="col-md-4">
                    <InputField
                      label="Número de Póliza *"
                      name="numeroPoliza"
                      placeholder="Ingrese el número de póliza"
                      disabled={isEditMode}
                      readOnly={isEditMode}
                    />
                  </div>
                  <div className="col-md-4">
                    <SelectField
                      label="Tipo de Póliza *"
                      name="tipoPolizaId"
                      options={policyTypes}
                    />
                  </div>
                  <div className="col-md-4">
                    <InputField
                      label="Cédula del Asegurado *"
                      name="cedulaAsegurado"
                      placeholder="Ingrese la cédula"
                      mask="9-9999-9999"
                    />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-4">
                    <InputField
                      label="Monto Asegurado *"
                      name="montoAsegurado"
                      type="number"
                      placeholder="Ingrese el monto"
                    />
                  </div>
                  <div className="col-md-4">
                    <DatePickerField
                      label="Fecha de Vencimiento *"
                      name="fechaVencimiento"
                    />
                  </div>
                  <div className="col-md-4">
                    <DatePickerField
                      label="Fecha de Emisión *"
                      name="fechaEmision"
                    />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-6">
                    <MultiSelectField
                      label="Coberturas *"
                      name="coberturasIds"
                      options={coverageOptions}
                    />
                  </div>
                  <div className="col-md-6">
                    <SelectField
                      label="Estado de Póliza *"
                      name="estadoPolizaId"
                      options={policyStates}
                    />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-4">
                    <InputField
                      label="Prima *"
                      name="prima"
                      type="number"
                      placeholder="Ingrese la prima"
                    />
                  </div>
                  <div className="col-md-4">
                    <DatePickerField label="Periodo *" name="periodo" />
                  </div>
                  <div className="col-md-4">
                    <DatePickerField
                      label="Fecha de Inclusión *"
                      name="fechaInclusion"
                    />
                  </div>
                </div>
                <div className="row">
                  <div className="col-md-6">
                    <InputField
                      label="Aseguradora *"
                      name="aseguradora"
                      placeholder="Ingrese la aseguradora"
                    />
                  </div>
                </div>
                <div className="d-flex justify-content-end gap-2 mt-4">
                  <button
                    type="button"
                    className="btn btn-secondary"
                    onClick={() => navigate("/policies")}
                  >
                    Cancelar
                  </button>
                  <button
                    type="submit"
                    className="btn btn-primary"
                    disabled={isSubmitting}
                  >
                    {isSubmitting ? "Guardando..." : "Guardar"}
                  </button>
                </div>
              </Form>
            )}
          </Formik>
        </div>
      </div>
    </div>
  );
};

export default PolicyForm;
