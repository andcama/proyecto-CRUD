import axios from 'axios';
import { API_URL } from '../utils/constants';

const api = axios.create({
  baseURL: API_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor de respuesta simple
api.interceptors.response.use(
  (response) => response,
  (error) => {
    // Solo registra errores sin lógica de actualización de token
    console.error('Error de API:', error);
    return Promise.reject(error);
  }
);

export default api;