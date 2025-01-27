import React, { useState } from "react";
import Card from "./Card";
import Subscribe from "./Subscribe";

/*********************************************************
 | Rev: 1.0 - created the component with some basic styling           | Author: Jan |
 | possibility of creating a title text for the card holder if it shows all courses from a specific category (for example: "View all of our cooking courses below")

 | Rev: 1.1 - added props, type is the type of data sent
 |          - moved data fetching to higher level comoponent
 |          - added the conditional for the + symbol                  | Author: Mihael |

 | Rev: 1.2 - added title (outer and one more inner div) and the title prop
 |          - changed some css in order to better fit the design      | Author: Mihael |
 *********************************************************/

export default function CardHolder({ reRender, title, data, type }) {
    const [subscriptionMenuState, setSubscriptionMenuState] = useState(false);
    const [creationMenuState, setCreationMenuState] = useState(false);

  return (
        <div className="flex flex-col border-black border-2 custom-shadow min-w-[308px] sm:min-w-[348px]">
            <div className="justify-start border-black border-b-[3px] border-dotted">
                <h1 className="text-4xl font-bold py-4 px-5">{title}</h1>
            </div>
            <div className="flex items-center overflow-x-auto h-auto overflow-y-hidden
                            custom-shadow custom-scrollbar sm:px-5 md:px-10 lg:px-15 xl:px-20">
        {
        /* array mapping to place the cards */
        data.map((data) => {
            return <Card 
                      key={type.includes('category') ? data.categoryId : data.course.courseId} 
                      title={type.includes('category') ? data.catName : data.course.courseName}
                   />
        })
        }
        {
        /*
         * For the 'profile' page
         * if type is either subscription or yourcourses then the + will be added
         * to the end of the list, on click it should open a pop up window which will
         * allow the user to add a new course or subscription to their account respectively
         */
        type === 'categorySubscriptions'
            ?  <p className="ml-14 text-7xl cursor-pointer" onClick={() => setSubscriptionMenuState(true)}>+</p>
            : null
        }
        {
        type === 'yourCourses'
            ? <p className="ml-14 text-7xl cursor-pointer" onClick={() => setCreationMenuState(true)}>+</p>
            : null
        }
      </div>
        {
        subscriptionMenuState ?
			<div className="flex justify-center">
                <Subscribe setMenuState={setSubscriptionMenuState}
                           reRender={reRender}
                />
			</div>
        :
            null
        }
        {
        creationMenuState ?
            null /* add component for creating a new course here */
        :
            null
        }
    </div>
  );
}