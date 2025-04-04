import * as Yup from 'yup';
import { ID_REGEX, NAME_MIN_LENGTH, NAME_MAX_LENGTH } from './constants';

// Esquema de validación de cliente
export const clientValidationSchema = Yup.object({
  cedulaAsegurado: Yup.string()
    .matches(ID_REGEX, 'La cédula debe tener 9 dígitos')
    .required('La cédula es requerida'),
  nombre: Yup.string()
    .min(NAME_MIN_LENGTH, `El nombre debe tener al menos ${NAME_MIN_LENGTH} caracteres`)
    .max(NAME_MAX_LENGTH, `El nombre debe tener máximo ${NAME_MAX_LENGTH} caracteres`)
    .required('El nombre es requerido'),
  primerApellido: Yup.string()
    .min(NAME_MIN_LENGTH, `El primer apellido debe tener al menos ${NAME_MIN_LENGTH} caracteres`)
    .max(NAME_MAX_LENGTH, `El primer apellido debe tener máximo ${NAME_MAX_LENGTH} caracteres`)
    .required('El primer apellido es requerido'),
  segundoApellido: Yup.string()
    .min(NAME_MIN_LENGTH, `El segundo apellido debe tener al menos ${NAME_MIN_LENGTH} caracteres`)
    .max(NAME_MAX_LENGTH, `El segundo apellido debe tener máximo ${NAME_MAX_LENGTH} caracteres`),
  tipoPersona: Yup.string()
    .required('El tipo de persona es requerido'),
  fechaNacimiento: Yup.date()
    .max(new Date(), 'La fecha de nacimiento no puede ser en el futuro')
    .required('La fecha de nacimiento es requerida'),
});

// Esquema de validación de póliza
export const policyValidationSchema = Yup.object({
  numeroPoliza: Yup.string()
    .required('El número de póliza es requerido'),
  tipoPolizaId: Yup.string()
    .required('El tipo de póliza es requerido'),
  cedulaAsegurado: Yup.string()
    .matches(ID_REGEX, 'La cédula debe tener 9 dígitos')
    .required('La cédula es requerida'),
  nombreCompleto: Yup.string()
    .required('El nombre completo es requerido'),
  montoAsegurado: Yup.number()
    .positive('El monto asegurado debe ser positivo')
    .required('El monto asegurado es requerido'),
  fechaVencimiento: Yup.date()
    .min(new Date(), 'La fecha de vencimiento debe ser en el futuro')
    .required('La fecha de vencimiento es requerida'),
  fechaEmision: Yup.date()
    .max(new Date(), 'La fecha de emisión no puede ser en el futuro')
    .required('La fecha de emisión es requerida'),
  coberturasIds: Yup.array()
    .min(1, 'Debe seleccionar al menos una cobertura')
    .required('Las coberturas son requeridas'),
  estadoPolizaId: Yup.string()
    .required('El estado de la póliza es requerido'),
  prima: Yup.number()
    .positive('La prima debe ser positiva')
    .required('La prima es requerida'),
  periodo: Yup.date()
    .required('El periodo es requerido'),
  fechaInclusion: Yup.date()
    .max(new Date(), 'La fecha de inclusión no puede ser en el futuro')
    .required('La fecha de inclusión es requerida'),
  aseguradora: Yup.string()
    .required('La aseguradora es requerida'),
});

// Esquema de validación de inicio de sesión
export const loginValidationSchema = Yup.object({
  username: Yup.string()
    .required('El nombre de usuario es requerido'),
  password: Yup.string()
    .min(6, 'La contraseña debe tener al menos 6 caracteres')
    .required('La contraseña es requerida'),
});