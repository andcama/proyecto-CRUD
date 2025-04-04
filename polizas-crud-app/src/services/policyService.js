import api from './api';

const policyService = {

  getAllPolicies: async () => {
    try {
      const response = await api.get('/Polizas');
      return response.data;
    } catch (error) {
      throw error;
    }
  },

  
  getPolicyById: async (id) => {
    try {
      const response = await api.get(`/Polizas/${id}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  },

  
  createPolicy: async (policyData) => {
    try {
      
      const response = await api.post('/Polizas', policyData);
      
      return response.data;
    } catch (error) {
      throw error;
    }
  },


  updatePolicy: async (id, policyData) => {
    try {
      const response = await api.put(`/Polizas/${id}`, policyData);
      return response.data;
    } catch (error) {
      throw error;
    }
  },

  deletePolicy: async (id) => {
    try {
      const response = await api.delete(`/Polizas/${id}`);
      return response.data;
    } catch (error) {
      throw error;
    }
  },

  // Search policies
  searchPolicies: async (params) => {
    try {

      const searchData = {
        NumeroPoliza: params.numeroPoliza || null,
        TipoPolizaId: params.tipoPolizaId ? parseInt(params.tipoPolizaId) : null,
        FechaVencimiento: params.fechaVencimiento ? new Date(params.fechaVencimiento).toISOString().split('T')[0]: null,
        CedulaAsegurado: params.cedulaAsegurado || null,
        NombreAsegurado: params.nombreAsegurado || null,
        PageNumber: params.pageNumber || 1,
        PageSize:params.pageSize || 10
      };
      const response = await api.post('/Polizas/search', searchData);
      return response.data;
    } catch (error) {
      throw error;
    }
  },


  getPolicyTypes: async () => {
    try {
      const response = await api.get('/Polizas/tipos');
      return response.data;
      
    } catch (error) {
      throw error;
    }
  },

  // Get all coverages
  getCoverages: async () => {
    try {
      const response = await api.get('/Polizas/coberturas');
      return response.data;
    } catch (error) {
      throw error;
    }
  },
  // Get all policy states
  getPolicyStates: async () => {
    try {
      const response = await api.get('/Polizas/estados');
      return response.data;
    } catch (error) {
      throw error;
    }
  },
};

export default policyService;