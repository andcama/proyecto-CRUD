import { useState, useEffect } from 'react';
import { useParams, Link } from 'react-router-dom';
import policyService from '../../services/policyService';
import { formatDate, formatCurrency, formatID, getPolicyStatusText, getPolicyTypeText } from '../../utils/formatters';

const PolicyDetail = () => {
  const { id } = useParams();
  const [policy, setPolicy] = useState(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState('');
  const [coverageOptions,setCoverageOptions]= useState();
  useEffect(() => {
    loadPolicy();
    loadCoverages();
  }, [id]);

  const loadCoverages = async () => {
    try {
      setLoading(true);
      const data = await policyService.getCoverages();
      setCoverageOptions(data);
    } catch (err) {
      setError('Error al cargar los detalles de la póliza');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const loadPolicy = async () => {
    try {
      setLoading(true);
      const data = await policyService.getPolicyById(id);
      setPolicy(data);
    } catch (err) {
      setError('Error al cargar los detalles de la póliza');
      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const getCoverageLabel = (value) => {
    const coverage = coverageOptions.find((option) => option.value === value);
    return coverage ? coverage.nombre : value;
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

  if (error) {
    return <div className="alert alert-danger">{error}</div>;
  }

  if (!policy) {
    return <div className="alert alert-warning">Póliza no encontrada</div>;
  }

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Detalles de Póliza</h2>
        <div>
          <Link to="/policies" className="btn btn-outline-secondary me-2">
            <i className="bi bi-arrow-left me-1"></i> Volver
          </Link>
          <Link to={`/policies/${id}/edit`} className="btn btn-primary">
            <i className="bi bi-pencil me-1"></i> Editar
          </Link>
        </div>
      </div>

      <div className="card mb-4">
        <div className="card-header bg-light">
          <h5 className="mb-0">Información de Póliza</h5>
        </div>
        <div className="card-body">
          <div className="row">
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Número de Póliza:</strong>
              <div>{policy.numeroPoliza}</div>
            </div>
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Tipo:</strong>
              <div>{getPolicyTypeText(policy.tipoPolizaNombre)}</div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Cédula del Asegurado:</strong>
              <div>{formatID(policy.cedulaAsegurado)}</div>
            </div>
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Estado:</strong>
              <div>
                <span className={`badge ${policy.estadoPolizaNombre === 'active' ? 'bg-success' : policy.estadoPolizaNombre === 'expired' ? 'bg-danger' : 'bg-warning'}`}>
                  {getPolicyStatusText(policy.estadoPolizaNombre)}
                </span>
              </div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Monto Asegurado:</strong>
              <div>{formatCurrency(policy.montoAsegurado)}</div>
            </div>
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Prima:</strong>
              <div>{formatCurrency(policy.prima)}</div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Fecha de Emisión:</strong>
              <div>{formatDate(policy.fechaEmision)}</div>
            </div>
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Fecha de Vencimiento:</strong>
              <div>{formatDate(policy.fechaVencimiento)}</div>
            </div>
          </div>

          <div className="row">
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Periodo:</strong>
              <div>{formatDate(policy.periodo)}</div>
            </div>
            <div className="col-md-6 mb-3">
              <strong className="d-block text-muted mb-1">Fecha de Inclusión:</strong>
              <div>{formatDate(policy.fechaInclusion)}</div>
            </div>
          </div>

          <div className="row">
            <div className="col-12 mb-3">
              <strong className="d-block text-muted mb-1">Aseguradora:</strong>
              <div>{policy.aseguradora}</div>
            </div>
          </div>
        </div>
      </div>

      <div className="card">
        <div className="card-header bg-light">
          <h5 className="mb-0">Coberturas</h5>
        </div>
        <div className="card-body">
          {policy.coberturas && policy.coberturas.length > 0 ? (
            <ul className="list-group">
              {policy.coberturas.map((coverage, index) => (
                <li key={index} className="list-group-item">
                  {getCoverageLabel(coverage)}
                </li>
              ))}
            </ul>
          ) : (
            <p className="mb-0 text-muted">No hay coberturas registradas.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default PolicyDetail;