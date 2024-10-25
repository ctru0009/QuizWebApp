import React, { useState } from "react";
import { toast } from "react-toastify";
import { useNavigate } from "react-router-dom";
import { BACKEND_URL } from "../constants";
const Auth = () => {
  const [isSignIn, setIsSignIn] = useState(true);
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [username, setUsername] = useState("");
  const navigate = useNavigate();

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    // Handle authentication logic here
    console.log({ email, password, username });
    if (isSignIn) {
      // Handle sign in
      const loginURL = `${BACKEND_URL}/api/users/login`;
      console.log(JSON.stringify({ username: username, password: password }));

      fetch(loginURL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({ username: username, password: password }),
      })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Invalid credentials");
            }
            return response.json();
        })
        .then((data) => {
          toast.success("Login successful");
          localStorage.setItem("ACCESS_TOKEN", data.token);
          // Redirect to home page
          navigate("/");
        })
        .catch((error) => {
          toast.error("Login failed: " + error);
          console.log(error);
        });
    } else {
      // Handle sign up
      const signupURL = `${BACKEND_URL}/api/Users/register`;
      fetch(signupURL, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: email,
          password: password,
          username: username,
        }),
      })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Invalid credentials");
            }
            return response.json();
        })
        .then((data) => {
          toast.success("Sign up successful");
          // Redirect to sign in page
          setIsSignIn(true);
          setEmail("");
          setPassword("");
          setUsername("");
        })
        .catch((error) => {
          toast.error("Sign up failed: " + error);
        });
    }
  };

  return (
    <div className="flex items-center justify-center min-h-screen bg-gray-100">
      <div className="bg-white p-8 rounded-lg shadow-md w-96">
        <h2 className="text-2xl font-bold text-center mb-6">
          {isSignIn ? "Sign In" : "Sign Up"}
        </h2>
        <form onSubmit={handleSubmit}>
          <div className="mb-4">
            <label className="block text-gray-700 mb-2" htmlFor="username">
              Username
            </label>
            <input
              type="text"
              id="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
              className="w-full p-2 border border-gray-300 rounded"
            />
          </div>
          {!isSignIn && (
            <div className="mb-4">
              <label className="block text-gray-700 mb-2" htmlFor="email">
                Email
              </label>
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
                className="w-full p-2 border border-gray-300 rounded"
              />
            </div>
          )}
          <div className="mb-4">
            <label className="block text-gray-700 mb-2" htmlFor="password">
              Password
            </label>
            <input
              type="password"
              id="password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              className="w-full p-2 border border-gray-300 rounded"
            />
          </div>
          <button
            type="submit"
            className="w-full bg-blue-500 text-white p-2 rounded hover:bg-blue-600"
          >
            {isSignIn ? "Sign In" : "Sign Up"}
          </button>
        </form>
        <p className="mt-4 text-center">
          {isSignIn ? "Don’t have an account?" : "Already have an account?"}
          <button
            onClick={() => setIsSignIn(!isSignIn)}
            className="text-blue-500 ml-1"
          >
            {isSignIn ? "Sign Up" : "Sign In"}
          </button>
        </p>
      </div>
    </div>
  );
};

export default Auth;
