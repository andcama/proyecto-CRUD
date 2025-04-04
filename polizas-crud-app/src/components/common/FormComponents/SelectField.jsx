import { useField } from 'formik';

const SelectField = ({
  label,
  name,
  options = [],
  placeholder = 'Seleccione una opción',
  className = '',
  disabled = false,
  onChange,
  ...props
}) => {
  const [field, meta, helpers] = useField(name);

  const handleChange = (e) => {
    // Call Formik's setValue
    helpers.setValue(e.target.value);
    
    // Call custom onChange handler if provided
    if (onChange) {
      onChange(e, helpers.form);
    }
  };

  return (
    <div className="mb-3">
      {label && (
        <label htmlFor={name} className="form-label">
          {label}
        </label>
      )}
      
      <select
        id={name}
        {...field}
        onChange={handleChange}
        className={`form-select ${className} ${meta.touched && meta.error ? 'is-invalid' : ''}`}
        disabled={disabled}
        {...props}
      >
        <option value="">{placeholder}</option>
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>
      
      {meta.touched && meta.error ? (
        <div className="invalid-feedback">{meta.error}</div>
      ) : null}
    </div>
  );
};

export default SelectField;