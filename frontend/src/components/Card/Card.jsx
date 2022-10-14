import React from "react";
import { observer } from "mobx-react-lite";
import { AnimationWrapper, CardWrapper } from "./Card.styles";

const Card = (props) => {
  const handleRefreshClick = (event) => {
    let className = "";
    if (props.isSuccess) className = "success";
    else className = "notsuccess";
    // Animate the clicked button (event.target)

    animateButton(event.target, className, 500);
  };

  const animateButton = (button, classNameAnimation, milliseconds) => {
    // Remove the class if it exists
    button.classList.remove(classNameAnimation);

    // Add the class
    button.classList.add(classNameAnimation);

    // When the animation finishes, remove the class
    setTimeout(() => {
      button.classList.remove(classNameAnimation);
      props.onClick(props.id);
    }, milliseconds);
  };
  return (
    <AnimationWrapper>
      <CardWrapper
        onClick={(e) => {
          handleRefreshClick(e);
          
        }}
      >
        {props.name}
      </CardWrapper>
    </AnimationWrapper>
  );
};

export default observer(Card);
