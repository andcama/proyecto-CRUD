import { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { Formik, Form, Field, ErrorMessage } from 'formik';
import { loginValidationSchema } from '../../utils/validators';
import { useAuth } from './AuthContext';

const Login = () => {
  const [error, setError] = useState('');
  const navigate = useNavigate();
  const { login } = useAuth();

  const handleSubmit = async (values, { setSubmitting }) => {
    try {
      const result = await login(values.username, values.password);
      if (result.success) {
        navigate('/');
      } else {
        setError(result.message);
      }
    } catch (err) {
      setError('Error al iniciar sesión. Inténtelo nuevamente.');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="d-flex justify-content-center align-items-center vh-100">
      <div className="card shadow p-4" style={{ width: '400px' }}>
        <h2 className="text-center mb-4">Iniciar Sesión</h2>
        
        {error && (
          <div className="alert alert-danger" role="alert">
            {error}
          </div>
        )}
        
        <Formik
          initialValues={{ username: '', password: '' }}
          validationSchema={loginValidationSchema}
          onSubmit={handleSubmit}
        >
          {({ isSubmitting }) => (
            <Form>
              <div className="mb-3">
                <label htmlFor="username" className="form-label">
                  Nombre de usuario
                </label>
                <Field
                  type="text"
                  id="username"
                  name="username"
                  className="form-control"
                  placeholder="Ingrese su nombre de usuario"
                />
                <ErrorMessage
                  name="username"
                  component="div"
                  className="text-danger mt-1"
                />
              </div>

              <div className="mb-4">
                <label htmlFor="password" className="form-label">
                  Contraseña
                </label>
                <Field
                  type="password"
                  id="password"
                  name="password"
                  className="form-control"
                  placeholder="Ingrese su contraseña"
                />
                <ErrorMessage
                  name="password"
                  component="div"
                  className="text-danger mt-1"
                />
              </div>

              <button
                type="submit"
                className="btn btn-primary w-100"
                disabled={isSubmitting}
              >
                {isSubmitting ? 'Iniciando sesión...' : 'Iniciar Sesión'}
              </button>
            </Form>
          )}
        </Formik>
      </div>
    </div>
  );
};

export default Login;