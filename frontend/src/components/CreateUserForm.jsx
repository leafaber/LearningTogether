import React from "react";
import InputLayout from "../layouts/InputLayout";

export default function CreateUserForm() {
  const [email, setEmail] = React.useState("");
  const [password, setPassword] = React.useState("");

  const handleSubmit = (event) => {
    event.preventDefault();

    console.log(`e-mail: ${email}, password ${password}`);

    fetch(`https://localhost:7002/api/register`, {
      method: "POST",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
      },
      mode: "cors",
      body: `email=${email}&password=${password}`,
    })
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        if (data.exists === false) {
          console.log("User created");
          setEmail("");
          setPassword("");
        } else {
          console.log("This email already exists");
        }
      })
      .catch((error) => console.log("An error ocurred:", error));
  };
  return (
    <InputLayout>
      <div className="flex flex-col gap-y-1 items-baseline z-10">
        <form
          className="flex flex-col gap-y-4 justify-center w-full"
          onSubmit={handleSubmit}
        >
          <input
            className="border-gray-500 border-2 border-radius rounded p-1 w-full"
            type="email"
            name="email"
            value={email}
            onChange={(event) => setEmail(event.target.value)}
            placeholder="Your e-mail"
            required
          />

          <input
            className="border-gray-500 border-2 border-radius rounded p-1 w-full"
            type="password"
            name="password"
            value={password}
            onChange={(event) => setPassword(event.target.value)}
            placeholder="Your password"
            required
          />

          <button
            className="border-gray-500 border-2 border-radius rounded p-1 bg-blue-500 text-white w-full md:w-2/4 self-center"
            type="submit"
            onClick={handleSubmit}
          >
            Create User
          </button>
        </form>
      </div>
    </InputLayout>
  );
}
