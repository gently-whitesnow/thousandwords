import React from "react";
import { observer } from "mobx-react-lite";
import { CardHolderWrapper, Line } from "./CardHolder.styles";
import Card from "../common/Card";

const CardHolder = () => {
  return (
    <CardHolderWrapper>
      <Line>
        <Card />
        <Card />
      </Line>
      <Line>
        <Card />
        <Card />
      </Line>
    </CardHolderWrapper>
  );
};

export default observer(CardHolder);
