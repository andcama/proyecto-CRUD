// Formatea fecha a cadena local (DD/MM/YYYY)
export const formatDate = (date) => {
  if (!date) return '';
  const dateObj = new Date(date);
  return dateObj.toLocaleDateString('es-ES', {
    day: '2-digit',
    month: '2-digit',
    year: 'numeric',
  });
};

// Formatea moneda (cantidad con 2 decimales y símbolo de moneda)
export const formatCurrency = (amount) => {
  if (amount === undefined || amount === null) return '';
  return new Intl.NumberFormat('es-CR', {
    style: 'currency',
    currency: 'CRC',
  }).format(amount);
};

// Formatea cédula con máscara (ej., 123456789 -> 1-2345-6789)
export const formatID = (id) => {
  if (!id) return '';
  
  const cleanId = id.replace(/\D/g, '');
  if (cleanId.length !== 9) return id;
  
  return `${cleanId.slice(0, 1)}-${cleanId.slice(1, 5)}-${cleanId.slice(5, 9)}`;
};

// Formatea número de póliza con ceros a la izquierda
export const formatPolicyNumber = (number) => {
  if (!number) return '';
  return number.toString().padStart(10, '0');
};

// Obtiene texto de estado de póliza basado en valor
export const getPolicyStatusText = (status) => {
  const statusMap = {
    active: 'Activo',
    expired: 'Vencido',
    canceled: 'Cancelado',
    pending: 'Pendiente',
  };
  
  return statusMap[status] || status;
};

// Obtiene texto de tipo de póliza basado en valor
export const getPolicyTypeText = (type) => {
  const typeMap = {
    auto: 'Automóvil',
    life: 'Vida',
    health: 'Salud',
    home: 'Hogar',
    travel: 'Viaje',
  };
  
  return typeMap[type] || type;
};