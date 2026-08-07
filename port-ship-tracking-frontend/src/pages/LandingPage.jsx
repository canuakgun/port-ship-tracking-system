import { Link } from "react-router-dom";

function LandingPage() {
  return (
    <div style={{ textAlign: "center", marginTop: "80px" }}>
      <h1>🚢 Port Ship Tracking System</h1>
      <p>Manage ships, ports, cargo, and crew assignments.</p>
      <div style={{ marginTop: "24px" }}>
        <Link to="/login"><button>Login</button></Link>
      </div>
    </div>
  );
}

export default LandingPage;