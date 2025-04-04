import { useField } from "formik";

const MultiSelectField = ({
  label,

  name,

  options = [],

  className = "",

  disabled = false,

  onChange,

  ...props
}) => {
  const [field, meta, helpers] = useField(name);

  const handleChange = (e) => {
    const values = Array.from(
      e.target.selectedOptions,

      (option) => option.value
    );

    helpers.setValue(values);

    // Call custom onChange handler if provided

    if (onChange) {
      onChange(values, helpers.form);
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
        multiple
        value={field.value || []}
        onChange={handleChange}
        onBlur={field.onBlur}
        className={`form-select ${className} ${
          meta.touched && meta.error ? "is-invalid" : ""
        }`}
        disabled={disabled}
        {...props}
      >
        {options.map((option) => (
          <option key={option.value} value={option.value}>
            {option.label}
          </option>
        ))}
      </select>

      <small className="form-text text-muted">
        Mantenga Ctrl presionado para seleccionar múltiples opciones.
      </small>

      {meta.touched && meta.error ? (
        <div className="invalid-feedback">{meta.error}</div>
      ) : null}
    </div>
  );
};

export default MultiSelectField;
