import React, { useState } from "react";
import { useNavigate } from "react-router-dom";

/**************************************************************
  | Rev: 1.0 - created the component | Author: Mihael | 

 **************************************************************/
export default function HeroText({ handleLogIn }) {
  const navigate = useNavigate();

  /*
   * Keep states of the input fields
   * 	- action - changes depending on which button was clicked to submit the
   * 	form
   *
   * *************************************************************************
   * Think of 'email' as a variable and 'setEmail' as a setter function for
   * that variable
   * *************************************************************************
   */
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");
  const [action, setAction] = React.useState("");

  const [response, setResponse] = useState("");

  /*
   * Function that handles the submit once one of the two buttons is pressed
   */
  const handleSubmit = (event) => {
    event.preventDefault();

    console.log(`Your e-mail: ${email}, Your password ${password}`);

    /*
     * Sends the form data to the backend depending on which button was
     * pressed to submit the form
     * 	- "register" - when the Register button is pressed
     * 	- "login" - when the Login button is pressed
     *
     * 	Once the data is sent the backend will send a response wich is taken
     * 	care of with .then()
     */
    fetch(`https://localhost:7002/api/${action}`, {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
      },
      mode: "cors",
      body: new URLSearchParams(`email=${email}&password=${password}`),
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        window.sessionStorage.setItem("token", data.token);
        if (data.exists === true && action === "login") {
          handleLogIn(data.role);
        } else if (data.exists === false && action === "login") {
          console.log("Wrong username or password");
        }

        if (data.exists === false && action === "register") {
          navigate("/profile");
        } else if (data.exists === true && action === "register") {
          console.log("This email already exists");
        }
      })
      .catch((error) => console.log("An error ocurred:", error));
  };

  /*
   * The HTML that is being returned when this component is called
   */
  return (
    <div className="flex flex-col gap-y-1 items-baseline">
      <h1 className="text-3xl">Learning together</h1>
      <span>Where the world learns</span>
      <form className="flex flex-col gap-y-4" onSubmit={handleSubmit}>
        {/*email input field
					- onChange - sets the 'email' input field to the email in
								 the field
				*/}
        <input
          className="border-gray-500 border-2 border-radius rounded p-1"
          type="email"
          name="email"
          value={email}
          onChange={(event) => setEmail(event.target.value)}
          placeholder="Your e-mail"
          required
        />
        {/*password input field
					- onChange - changes the 'password' state when the value in
								 the field is changed
				*/}
        <input
          className="border-gray-500 border-2 border-radius rounded p-1"
          type="password"
          name="password"
          value={password}
          onChange={(event) => setPassword(event.target.value)}
          placeholder="Your password"
          required
        />
        <div className="flex justify-around">
          {/*register button
						- onClick - sets the 'action' state to "register"
					*/}
          <button
            className="border-gray-500 border-2 border-radius rounded p-1"
            type="submit"
            onClick={() => setAction("register")}
          >
            Register
          </button>
          {/*login button
						- onClick - sets the 'action' state to "login"
					*/}
          <button
            className="border-gray-500 border-2 border-radius rounded p-1 bg-blue-500 text-white"
            type="submit"
            onClick={() => setAction("login")}
          >
            Login
          </button>
        </div>
      </form>
    </div>
  );
}
