import React, { useState, useEffect } from "react";
import jwtDecode from "jwt-decode";

export default function ProfileEdit({ closeMenu }) {
    const currentData = jwtDecode(window.sessionStorage.getItem('token'));

    const [password, setPassword] = useState('');
    const [email, setEmail] = useState(currentData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress']);
    const [phone, setPhone] = useState(currentData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/mobilephone']);
    const [firstName, setFirstName] = useState(currentData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name']);
    const [lastName, setLastName] = useState(currentData['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname']);

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = new FormData();
        formData.append('password', password);
        formData.append('firstName', firstName);
        formData.append('lastName', lastName);
        formData.append('phoneNumber', phone);

        console.log(formData);

        fetch('https://localhost:7002/api/users/edit', {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + window.sessionStorage.getItem('token')
            },
            mode: 'cors',
            body: formData
        }).then(response => response.json())
          .then(json => {
            console.log(json.token);
            console.log(jwtDecode(json.token));
            window.sessionStorage.setItem('token', json.token)
        })
          .catch('Wrong data has been input')

        closeMenu();
    }

    return (
        <div className="z-50 fixed h-[80%] w-[95%] top-[10%] bg-white border border-black overflow-y-scroll">
            <h1 className="text-4xl font-semibold mt-7 ml-10">Profile settings</h1>
            <h1 className="text-3xl mt-3 ml-20">{email}</h1>
            <hr className="h-[2px] bg-black mt-4"/>
            <form className="relative h-[82.6%] flex min-h-min flex-col justify-between" onSubmit={handleSubmit}>
                <div className="flex flex-col gap-16 sm:gap-[20%] py-10 px-[15%] sm:grid sm:grid-cols-2 bg-white">

                    <div className="z-0 flex flex-col gap-10">
                        <h1 className="text-3xl">Private information</h1>
                        <div className="flex flex-col">
                            <label className="text-xl" htmlFor="password">Your new password:</label>
                            <input className="border border-black text-xl px-2 py-1"
                                    type="password"
                                    name="password" 
                                    id="password"
                                    value={password}
                                    onChange={(event) => setPassword(event.target.value)}
                            />
                        </div>
                        <div className="flex flex-col">
                            <label className="text-xl" htmlFor="phone">Your phone:</label>
                            <input className="border border-black text-xl px-2 py-1"
                                    type="tel" 
                                    pattern="[0-9]*"
                                    name="phone" 
                                    id="phone"
                                    placeholder={phone}
                                    onChange={(event) => setPhone(event.target.value)}
                            />
                        </div>
                    </div>

                    <div className="z-0 flex flex-col gap-10">
                        <h1 className="text-3xl">Public information</h1>
                        <div className="flex flex-col">
                            <label className="text-xl" htmlFor="firstname">Your first name:</label>
                            <input className="border border-black text-xl px-2 py-1" 
                                    type="text" 
                                    name="firstname" 
                                    id="firstname"
                                    placeholder={firstName}
                                    onChange={(event) => setFirstName(event.target.value)}
                            />
                        </div>
                        <div className="flex flex-col">
                            <label className="text-xl" htmlFor="lastname">Your last name:</label>
                            <input className="border border-black text-xl px-2 py-1"
                                    type="text" 
                                    name="lastname" 
                                    id="lastname"
                                    placeholder={lastName}
                                    onChange={(event) => setLastName(event.target.value)}
                            />
                        </div>
                    </div>

                </div>
                <div className="relative bottom-0 grid grid-cols-2 border-t border-black bg-white">
                    <button className="border-r border-black py-3 text-xl"
                            onClick={() => closeMenu()}
                    >
                        Close
                    </button>
                    <button className="py-3 text-xl" 
                            type="submit"
                    >
                        Save
                    </button>
                </div>
            </form>
        </div>
    );
}