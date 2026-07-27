function ErrorMessage({ message, onDismiss }) {
  if (!message) return null;

  return (
    <div className="error-banner">
      <span>{message}</span>
      {onDismiss && (
        <button className="error-dismiss" onClick={onDismiss}>
          ×
        </button>
      )}
    </div>
  );
}

export default ErrorMessage;