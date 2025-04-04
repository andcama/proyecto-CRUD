import { Field, ErrorMessage } from 'formik';

const InputField = ({
  label,
  name,
  type = 'text',
  placeholder = '',
  className = '',
  disabled = false,
}) => {
  return (
    <div className="mb-3">
      {label && (
        <label htmlFor={name} className="form-label">
          {label}
        </label>
      )}
      
      <Field
        id={name}
        name={name}
        type={type}
        placeholder={placeholder}
        className={`form-control ${className}`}
        disabled={disabled}
      />
      
      <ErrorMessage
        name={name}
        component="div"
        className="text-danger mt-1 small"
      />
    </div>
  );
};

export default InputField;