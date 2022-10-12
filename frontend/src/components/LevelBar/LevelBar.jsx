import React from "react";
import { observer } from "mobx-react-lite";
import { Bar, LevelBarWrapper, BarGround, BarLighter } from "./LevelBar.styles";

const LevelBar = () => {
  return (
    <LevelBarWrapper>
      <BarGround />
      <BarLighter />
      <Bar />
    </LevelBarWrapper>
  );
};

export default observer(LevelBar);
