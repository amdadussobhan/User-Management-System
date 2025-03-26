import "./App.css";
import Login from "./Components/Auth/Login";
import List from "./Components/User/List";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  const handleLogin = (credentials) => {
    console.log("Logging in with:", credentials);
    // You can perform your login logic or API request here
  };

  return (
    <div>
      <Login onLogin={handleLogin} />
    </div>
  );
}

export default App;
