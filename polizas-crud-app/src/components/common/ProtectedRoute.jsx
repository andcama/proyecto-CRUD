import { Navigate, Outlet } from 'react-router-dom';
import { useAuth } from '../auth/AuthContext';
import Navbar from './Navbar';

const ProtectedRoute = () => {
  const { isAuthenticated, loading } = useAuth();

  // Mostrar carga o redirigir si no está autenticado
  if (loading) {
    return <div>Cargando...</div>;
  }

  // Ya que la autenticación JWT fue removida del backend, solo verificamos el usuario local
  // Esta es una verificación de autenticación más simple, ya que la verificación de token en el servidor no existe
  if (!isAuthenticated) {
    return <Navigate to="/login" />;
  }

  // Si está autenticado, renderizar las rutas hijas con el diseño
  return (
    <div>
      <Navbar />
      <div className="container mt-4">
        <Outlet />
      </div>
    </div>
  );
};

export default ProtectedRoute;