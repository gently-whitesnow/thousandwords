import React from "react";
import { observer } from "mobx-react-lite";
import { CustomWord, MindWordWrapper } from "./MindWord.styles";

const MindWord = (props) => {
  return (
    <MindWordWrapper onClick={props.onClick}>
      <CustomWord>{props.word}</CustomWord>
    </MindWordWrapper>
  );
};

export default observer(MindWord);
