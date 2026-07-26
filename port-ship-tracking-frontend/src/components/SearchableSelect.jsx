import { useState, useEffect, useRef } from "react";

function SearchableSelect({ options, value, onChange, placeholder }) {
  const [query, setQuery] = useState("");
  const [isOpen, setIsOpen] = useState(false);
  const wrapperRef = useRef(null);

  // Dışarı tıklanınca dropdown'ı kapat
  useEffect(() => {
    const handleClickOutside = (e) => {
      if (wrapperRef.current && !wrapperRef.current.contains(e.target)) {
        setIsOpen(false);
      }
    };
    document.addEventListener("mousedown", handleClickOutside);
    return () => document.removeEventListener("mousedown", handleClickOutside);
  }, []);

  const filteredOptions = query
    ? options.filter((opt) => opt.id.toString().includes(query))
    : options;

  const handleSelect = (option) => {
    onChange(option.id);
    setQuery(`${option.id} - ${option.label}`);
    setIsOpen(false);
  };

  return (
    <div ref={wrapperRef} style={{ position: "relative", display: "inline-block" }}>
      <input
        type="text"
        placeholder={placeholder}
        value={query}
        onChange={(e) => {
          setQuery(e.target.value);
          setIsOpen(true);
        }}
        onFocus={() => setIsOpen(true)}
      />
      {isOpen && filteredOptions.length > 0 && (
        <ul
          style={{
            position: "absolute",
            top: "100%",
            left: 0,
            zIndex: 10,
            background: "white",
            border: "1px solid #ccc",
            listStyle: "none",
            margin: 0,
            padding: 0,
            maxHeight: "150px",
            overflowY: "auto",
            width: "200px",
          }}
        >
          {filteredOptions.map((opt) => (
            <li
              key={opt.id}
              onClick={() => handleSelect(opt)}
              style={{ padding: "4px 8px", cursor: "pointer" }}
            >
              {opt.id} - {opt.label}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
}

export default SearchableSelect;