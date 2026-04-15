import React, { useState, useEffect } from "react";
import "../custom.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { toast } from "react-toastify";
import { API_BASE_URL } from "../constants";
import LoginResponse from "../models/LoginResponse";

function Login() {

 useEffect(() => {
    const rememberedUsername = localStorage.getItem("rememberedUsername");
    const rememberedPassword = localStorage.getItem("rememberedPassword");
    if (rememberedUsername && rememberedPassword) {
        setUsername(rememberedUsername);
        setPassword(rememberedPassword);
        setRememberMe(true);
    }
}, []);


  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [rememberMe, setRememberMe] = useState(false);
  const [error, setError] = useState({ message: "" });
  const [showPassword, setShowPassword] = useState(false);
  const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&]).{8,}$/;

  const handleSubmit = async (
    e: React.FormEvent<HTMLFormElement>,
  ): Promise<void> => {
    e.preventDefault();

    // ✅ Validation
    if (!username && !password) {
      setError({ message: "Username and password are required" });
      return;
    }

    if (!username) {
      setError({ message: "Username is required" });
      return;
    }

    if (!password) {
      setError({ message: "Password is required" });
      return;
    }

    if (!passwordRegex.test(password)) {
      setError({
        message:
          "Password must be at least 8 characters long and include uppercase, lowercase, number, and special character",
      });
      return;
    }

    try {
      const response: Response = await fetch(API_BASE_URL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify({
          query: `
                    mutation {
                        login(query: { email: "${username}", password: "${password}" })
                    }
                `,
        }),
      });

      if (!response.ok) {
        setError({
          message: `Server error: ${response.status} ${response.statusText}`,
        });
        return;
      }

      const data: LoginResponse = await response.json();

      if (data.errors && data.errors.length > 0) {
        setError({ message: data.errors[0].message });
        return;
      }

      if (!data.data?.login) {
        setError({
          message: "Invalid response from server. Please try again.",
        });
        return;
      }

      const token = data.data.login;
      localStorage.setItem("token", token);

      if (rememberMe) {
        localStorage.setItem("rememberedUsername", username);
        localStorage.setItem("rememberedPassword", password);
      } else {
        localStorage.removeItem("rememberedUsername");
        localStorage.removeItem("rememberedPassword");
      }

      toast.success("Login successful!");

      // navigate("/dashboard");
    } catch (err: unknown) {
      const message =
        err instanceof Error
          ? err.message
          : "Failed to connect to server. Please try again.";

      setError({ message });
    }
  };

  return (
    <div className="login-container">
      <h2>Login</h2>
      <div className="row">
        <div className="col-md-12">
          {error.message && (
            <div className="alert alert-danger">{error.message}</div>
          )}
          <form onSubmit={handleSubmit}>
            <input
              type="text"
              placeholder="Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
            />
            <input
              type={showPassword ? "text" : "password"}
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
            />
            <div
              className="form-check mb-3"
              style={{
                display: "flex",
                alignItems: "center",
                gap: "6px",
                marginBottom: "1rem",
                paddingLeft: 0,
              }}
            >
              <input
                className="form-check-input"
                type="checkbox"
                id="rememberMe"
                style={{ margin: 0 }}
                checked={rememberMe}
                onChange={(e) => setRememberMe(e.target.checked)}   
              />
              <label
                className="form-check-label"
                htmlFor="rememberMe"
                style={{ margin: 0, paddingLeft: 0 }}
              >
                Remember me
              </label>
            </div>
            <button type="submit" className="btn btn-primary w-100">
              Login
            </button>
          </form>
        </div>
      </div>
    </div>
  );
}

export default Login;
