import { useState, useEffect } from "react";
import { Formik, Form } from "formik";
import { policyValidationSchema } from "../../utils/validators";

import InputField from "../common/FormComponents/InputField";
import SelectField from "../common/FormComponents/SelectField";
import DatePickerField from "../common/FormComponents/DatePicker";
import policyService from "../../services/policyService";
import { data } from "react-router-dom";

const PolicySearch = ({ onSearch, onClear }) => {
  const [isExpanded, setIsExpanded] = useState(false);
  const [searching, setSearching] = useState(false);
  const [error, setError] = useState("");
  const [policyTypes, setPolicyTypes] = useState([]);

  const initialValues = {
    numeroPoliza: "",
    tipoPolizaId: "",
    cedulaAsegurado: "",
    fechaVencimiento: null,
    nombreAsegurado: "",
  };
  useEffect(() => {
    // Load data
    const loadData = async () => {
      try {
        loadPolicyTypes();
      } catch (err) {
        console.error("Error loading data:", err);
      } finally {
      }
    };
    loadData();
  }, []);
  const loadPolicyTypes = async () => {
    try {
      const types = await policyService.getPolicyTypes();
      const formattedTypes = types.map((type) => ({
        value: type.id,
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

  const handleSubmit = async (values) => {
    try {
      setSearching(true);
      setError("");

      // Remove empty values
      const searchParams = Object.fromEntries(
        Object.entries(values).filter(
          ([_, value]) => value !== null && value !== undefined && value !== ""
        )
      );

      // Only search if at least one field has a value
      if (Object.keys(searchParams).length === 0) {
        setError("Ingrese al menos un criterio de búsqueda");
        return;
      }

      const results = await policyService.searchPolicies(searchParams);

      if (results.items && results.items.length === 0) {
        setError("No se encontraron polizas");
      }
      onSearch(results);
    } catch (err) {
      setError("Error al realizar la búsqueda");
      console.error(err);
    } finally {
      setSearching(false);
    }
  };

  const handleClear = (resetForm) => {
    resetForm();
    onClear();
  };

  return (
    <div className="card mb-4">
      <div className="card-header bg-light d-flex justify-content-between align-items-center">
        <h5 className="mb-0">
          <button
            className="btn btn-link text-decoration-none p-0"
            onClick={() => setIsExpanded(!isExpanded)}
          >
            <i
              className={`bi bi-chevron-${isExpanded ? "up" : "down"} me-2`}
            ></i>
            Buscar Pólizas
          </button>
        </h5>
      </div>

      <div className={`card-body ${isExpanded ? "" : "d-none"}`}>
        {error && (
          <div className="alert alert-danger" role="alert">
            {error}
          </div>
        )}

        <Formik initialValues={initialValues} onSubmit={handleSubmit}>
          {({ resetForm }) => (
            <Form>
              <div className="row">
                <div className="col-md-4">
                  <InputField
                    label="Número de Póliza"
                    name="numeroPoliza"
                    placeholder="Ingrese el número de póliza"
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
                    label="Cédula del Asegurado"
                    name="cedulaAsegurado"
                    placeholder="Ingrese la cédula"
                    mask="9-9999-9999"
                  />
                </div>
              </div>

              <div className="row">
                <div className="col-md-6">
                  <DatePickerField
                    label="Fecha de Vencimiento"
                    name="fechaVencimiento"
                  />
                </div>

                <div className="col-md-6">
                  <InputField
                    label="Nombre del Asegurado"
                    name="nombreAsegurado"
                    placeholder="Ingrese el nombre"
                  />
                </div>
              </div>

              <div className="d-flex gap-2 mt-3">
                <button
                  type="submit"
                  className="btn btn-primary"
                  disabled={searching}
                >
                  {searching ? (
                    <>
                      <span
                        className="spinner-border spinner-border-sm me-2"
                        role="status"
                        aria-hidden="true"
                      ></span>
                      Buscando...
                    </>
                  ) : (
                    <>
                      <i className="bi bi-search me-2"></i>
                      Buscar
                    </>
                  )}
                </button>

                <button
                  type="button"
                  className="btn btn-outline-secondary"
                  onClick={() => handleClear(resetForm)}
                >
                  <i className="bi bi-x-circle me-2"></i>
                  Limpiar
                </button>
              </div>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default PolicySearch;
