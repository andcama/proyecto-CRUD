import { useState, useEffect } from "react";

import { Link } from "react-router-dom";

import policyService from "../../services/policyService";

import {
  formatDate,
  formatCurrency,
  formatID,
  getPolicyStatusText,
  getPolicyTypeText,
} from "../../utils/formatters";

import PolicySearch from "./PolicySearch";

const PolicyList = () => {
  const [policies, setPolicies] = useState([]);

  const [loading, setLoading] = useState(true);

  const [error, setError] = useState("");

  const [searchResults, setSearchResults] = useState(null);

  useEffect(() => {
    // Load policies on component mount

    loadPolicies();
  }, []);

  const loadPolicies = async () => {
    try {
      setLoading(true);

      const data = await policyService.getAllPolicies();

      setPolicies(data);

      setSearchResults(null);
    } catch (err) {
      setError("Error al cargar las pólizas");

      console.error(err);
    } finally {
      setLoading(false);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm("¿Está seguro que desea eliminar esta póliza?")) {
      try {
        await policyService.deletePolicy(id);

        // Reload policies after deletion

        loadPolicies();
      } catch (err) {
        setError("Error al eliminar la póliza");

        console.error(err);
      }
    }
  };

  const handleSearch = async (results) => {
    // Check if results exist

    if (results) {
      // If there are no items property or it's empty, set a properly structured empty search results

      if (!results.items || results.items.length === 0) {
        setSearchResults({ items: [] });
      } else {
        setSearchResults(results);
      }
    }
  };

  const handleClearSearch = () => {
    setSearchResults(null);

    setError(""); // Clear any previous error messages
  };

  // When in search mode, display searchResults.items, otherwise show all policies

  const displayPolicies = searchResults ? searchResults.items : policies;

  return (
    <div>
      <div className="d-flex justify-content-between align-items-center mb-4">
        <h2>Listado de Pólizas</h2>

        <Link to="/policies/new" className="btn btn-primary">
          <i className="bi bi-plus-circle me-2"></i>Nueva Póliza
        </Link>
      </div>

      <PolicySearch onSearch={handleSearch} onClear={handleClearSearch} />

      {error && <div className="alert alert-danger">{error}</div>}

      {loading ? (
        <div className="text-center my-5">
          <div className="spinner-border" role="status">
            <span className="visually-hidden">Cargando...</span>
          </div>
        </div>
      ) : displayPolicies.length === 0 ? (
        <div className="alert alert-info">
          {searchResults
            ? "No se encontraron resultados para su búsqueda."
            : "No hay pólizas registradas."}
        </div>
      ) : (
        <div className="table-responsive">
          <table className="table table-striped table-hover">
            <thead className="table-light">
              <tr>
                <th>Número Póliza</th>

                <th>Tipo</th>

                <th>Cédula Asegurado</th>

                <th>Monto Asegurado</th>

                <th>Vencimiento</th>

                <th>Estado</th>

                <th>Prima</th>

                <th>Acciones</th>
              </tr>
            </thead>

            <tbody>
              {displayPolicies.map((policy) => (
                <tr key={policy.id}>
                  <td>{policy.numeroPoliza}</td>

                  <td>{getPolicyTypeText(policy.tipoPolizaNombre)}</td>

                  <td>{formatID(policy.cedulaAsegurado)}</td>

                  <td>{formatCurrency(policy.montoAsegurado)}</td>

                  <td>{formatDate(policy.fechaVencimiento)}</td>

                  <td>
                    <span
                      className={`badge ${
                        policy.estadoPolizaNombre === "active"
                          ? "bg-success"
                          : policy.estadoPolizaNombre === "expired"
                          ? "bg-danger"
                          : "bg-warning"
                      }`}
                    >
                      {getPolicyStatusText(policy.estadoPolizaNombre)}
                    </span>
                  </td>

                  <td>{formatCurrency(policy.prima)}</td>

                  <td>
                    <div className="btn-group btn-group-sm">
                      <Link
                        to={`/policies/${policy.numeroPoliza}`}
                        className="btn btn-info"
                        title="Ver detalles"
                      >
                        <i className="bi bi-eye"></i>
                      </Link>

                      <Link
                        to={`/policies/${policy.numeroPoliza}/edit`}
                        className="btn btn-warning"
                        title="Editar"
                      >
                        <i className="bi bi-pencil"></i>
                      </Link>

                      <button
                        className="btn btn-danger"
                        onClick={() => handleDelete(policy.numeroPoliza)}
                        title="Eliminar"
                      >
                        <i className="bi bi-trash"></i>
                      </button>
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
  );
};

export default PolicyList;
