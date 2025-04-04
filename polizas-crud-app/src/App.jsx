import { BrowserRouter as Router, Routes, Route, Navigate } from 'react-router-dom';
import { AuthProvider } from './components/auth/AuthContext';
import ProtectedRoute from './components/common/ProtectedRoute';

// Auth Components
import Login from './components/auth/Login';

// Polizas Components
import PolicyList from './components/policies/PolicyList';
import PolicyForm from './components/policies/PolicyForm';
import PolicyDetail from './components/policies/PolicyDetail';

// Import  CSS
import './custom.css'
import './App.css'

function App() {
  return (
    <AuthProvider>
      <Router>
        <Routes>
          {/* Public Routes */}
          <Route path="/login" element={<Login />} />
          
          {/* Protected Routes */}
          <Route element={<ProtectedRoute />}>
            <Route path="/" element={<Navigate to="/policies" replace />} />
            <Route path="/policies" element={<PolicyList />} />
            <Route path="/policies/new" element={<PolicyForm />} />
            <Route path="/policies/:id" element={<PolicyDetail />} />
            <Route path="/policies/:id/edit" element={<PolicyForm />} />
          </Route>
          
          {/* Fallback Route */}
          <Route path="*" element={<Navigate to="/" replace />} />
        </Routes>
      </Router>
    </AuthProvider>
  );
}

export default App;