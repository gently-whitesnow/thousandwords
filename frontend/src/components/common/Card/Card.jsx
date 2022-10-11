import React from "react";
import { observer } from "mobx-react-lite";
import { CardWrapper, Word } from "./Card.styles";

const Card = () => {
  return (
    <CardWrapper>
      <Word>Hello</Word>
    </CardWrapper>
  );
};

export default observer(Card);
