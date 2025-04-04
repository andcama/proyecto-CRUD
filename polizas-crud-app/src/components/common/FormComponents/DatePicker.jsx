import { useField } from 'formik';
import ReactDatePicker from 'react-datepicker';
import 'react-datepicker/dist/react-datepicker.css';

const DatePickerField = ({
  label,
  name,
  className = '',
  disabled = false,
  ...props
}) => {
  const [field, meta, helpers] = useField(name);

  return (
    <div className="mb-3">
      {label && (
        <label htmlFor={name} className="form-label">
          {label}
        </label>
      )}
      
      <ReactDatePicker
        id={name}
        selected={field.value ? new Date(field.value) : null}
        onChange={(date) => helpers.setValue(date)}
        className={`form-control ${className}`}
        dateFormat="dd/MM/yyyy"
        disabled={disabled}
        {...props}
      />
      
      {meta.touched && meta.error ? (
        <div className="text-danger mt-1 small">{meta.error}</div>
      ) : null}
    </div>
  );
};

export default DatePickerField;