import React from "react";
import { useState, useEffect } from "react";
import CardHolder from "../components/CardHolder";
import Layout from "../layouts/Layout";

/********************************************************
 * | Rev: 1.0 - created | Author: Jan |
 *
 * | Rev: 1.1 - added elements | Author: Mihael |
 *
 *******************************************************/
export default function Profile({ isLoggedIn }) {
	const [recommendedCourses, setRecommendedCourses] = useState([]);
	const [enrolledCourses, setEnrolledCourses] = useState([]);
	const [subscribedCategories, setSubscribedCategories] = useState([]);
	const [ownCourses, setOwnCourses] = useState([]);
	const [changed, setChanged] = useState(0);

	const reRender = (prev) => {
		setChanged(prev + 1);
	}

  useEffect(() => {
    //fetch recommended courses
    fetch("https://localhost:7002/api/courses/recommended", {
      method: "GET",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        /*
         * User authentication via this header, 'Bearer ' is a must
         */
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
    })
      .then((response) => response.json())
      .then((data) => setRecommendedCourses(data));

    //fetch enrolled courses
    fetch("https://localhost:7002/api/enrolled/all", {
      method: "GET",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        /*
         * User authentication via this header, 'Bearer ' is a must
         */
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
    })
      .then((response) => response.json())
      .then((data) => setEnrolledCourses(data));

    //fetch subscriptions
    fetch("https://localhost:7002/api/categories/subscribed", {
      method: "GET",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        /*
         * User authentication via this header, 'Bearer ' is a must
         */
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
    })
      .then((response) => response.json())
      .then((data) => setSubscribedCategories(data));

    //fetch courses that the user has created
    fetch("https://localhost:7002/api/courses/own", {
      method: "GET",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded;charset=UTF-8",
        "Access-Control-Allow-Origin": "*",
        /*
         * User authentication via this header, 'Bearer ' is a must
         */
        Authorization: "Bearer " + window.sessionStorage.getItem("token"),
      },
      mode: "cors",
    })
      .then((response) => response.json())
      .then((data) => setOwnCourses(data));

		setChanged();

	}, [changed]);

  return (
    subscribedCategories ? 
      <Layout current={"Profile"} isLoggedIn={isLoggedIn}>
        <div className="mt-[26vh] sm:mt-[10%]">
          <CardHolder
            reRender={reRender}
            title="Courses for you"
            data={recommendedCourses}
            type="recommendedCourses"
          />
        </div>

        <div className="flex justify-center items-center">
          <span className="h-96 border-r-[3px] border-dotted border-black"></span>
        </div>

        <CardHolder
          reRender={reRender}
          title="Courses you enrolled"
          data={enrolledCourses}
          type="enrolledCourses"
        />

        <div className="flex justify-center items-center">
          <span className="h-96 border-r-[3px] border-dotted border-black"></span>
        </div>

        <CardHolder
          reRender={reRender}
          title="Your subscriptions"
          data={subscribedCategories}
          type="categorySubscriptions"
        />

        <div className="flex justify-center items-center">
          <span className="h-96 border-r-[3px] border-dotted border-black"></span>
        </div>

        <CardHolder 
          title="Your courses" 
          data={ownCourses} 
          type="yourCourses"
        />
        <div className="flex justify-center items-center">
          <span className="h-96 border-r-[3px] border-dotted border-black"></span>
        </div>
      </Layout>
    :
      <h1>Loading...</h1>
  );
}
