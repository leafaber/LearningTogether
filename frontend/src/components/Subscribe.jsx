import React, { useState, useEffect } from "react";
import Card from "./Card";

/**************************************************************
 | Rev: 1.0 created the component | Author: Mihael |
 
   
 **************************************************************/
export default function Subscribe({ reRender, setMenuState }) {
    const [notSubscribed, setNotSubscribed] = useState([]);
    const [subscribeTo, setSubscribeTo] = useState();

    useEffect(() => {
        /*
         * Fetches all the courses that the user has not yet subscribed to
         */
        fetch('https://localhost:7002/api/categories/notsubscribed', {
            method: 'GET',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + window.sessionStorage.getItem('token')
            },
            mode: 'cors'
        })
            .then(response => response.json())
            .then(json => setNotSubscribed(json));
    }, []);

    const handleSubmit = () => {
        const formData = new FormData();
        formData.append('categoryName', subscribeTo);
        
        /*
         * Sends the data to the endpoint for new subscription
         */
        fetch('https://localhost:7002/api/categories/subscribed', {
            method: 'POST',
            headers: {
                'Access-Control-Allow-Origin': '*',
                'Authorization': 'Bearer ' + window.sessionStorage.getItem('token')
            },
            mode: 'cors',
            body: formData
        })
            .then(response => response.status === 200)
            .then(console.log('Subscribed sucessfully'))
            .catch((error) => console.log(error));

        // close the window after the data has been sent
        setMenuState(false);
    }

    return (
        notSubscribed ?
            <form className="fixed flex flex-col h-[80%] w-[95%] bottom-[10%] justify-between bg-white border border-black"
                    onSubmit={handleSubmit}>
                    <div className="flex flex-col">
                        <h1 className="text-3xl font-semibold mt-5 mb-4 ml-5">
                            Subscribe to a category
                        </h1>
                        <hr className="h-[2px] bg-black" />
                    </div>
                    <div className="flex flex-wrap justify-evenly items-center py-11 overflow-y-scroll">
                        <div className="grid grid-cols-1 gap-10 sm:grid-cols-2 px-12 md:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 overflow-y-scroll">
                            {
                                //displays all the courses that the user has not yet subscribed to
                                notSubscribed.length !== 0 ?
                                    notSubscribed.map(category => {
                                        return <button type="submit" 
                                                    key={category.categoryId} 
                                                    onClick={() => {
                                                        setSubscribeTo(category.catName);
                                                        reRender();
                                                    }}>
                                                    <Card title={category.catName} />
                                                </button>
                                    })
                                :
                                    <h1 className="grid grid-cols-1 text-2xl font-semibold">
                                        <span className="font-bold">Congratulations!</span> You've got them all.
                                    </h1>
                            }
                        </div>
                    </div>
                    <div className="grid grid-cols-1 border-t border-black">
                        <button className="text-xl py-4" onClick={() => setMenuState(false)}>
                            Cancel
                        </button>
                    </div>
            </form>
        :
            <h1>Loading...</h1>
    );
}