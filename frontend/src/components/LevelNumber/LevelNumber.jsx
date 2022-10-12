import React from "react";
import { observer } from "mobx-react-lite";
import { CustomNumber, LevelNumberWrapper } from "./LevelNumber.styles";

const LevelNumber = (props) => {
  return (
    <LevelNumberWrapper>
      <CustomNumber>{props.level}</CustomNumber>
    </LevelNumberWrapper>
  );
};

export default observer(LevelNumber);
