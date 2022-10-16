import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { CardHolderWrapper, Line } from "./CardHolder.styles";
import { useStore } from "../../store";
import { shuffle } from "../../store/wordsStore";
import Card from "../Card";

const CardHolder = () => {
  const { wordsStore } = useStore();
  const {
    getWord,
    setWord,
    getOtherWords,
    isNative,
    currentWord,
    tempOtherWords,
    wordsQueue,
  } = wordsStore;

  const clickHandler = (word_id) => {
    if (word_id === currentWord.word.word_id) {
      currentWord.count++;
      setWord(currentWord);
    } else {
      currentWord.count = 0;
      setWord(currentWord);
    }
    getWord();
    getOtherWords();
    console.log(wordsQueue);
    wordsQueue.map((e) => console.log(e.count));
  };

  console.log(currentWord);
  console.log(tempOtherWords);
  let cards = [
    <Card
      name={isNative ? currentWord.word.n_lang : currentWord.word.f_lang}
      onClick={clickHandler}
      id={currentWord.word.word_id}
      isSuccess={true}
      trueName={isNative ? currentWord.word.n_lang : currentWord.word.f_lang}
    />,
    <Card
      name={isNative ? tempOtherWords[0].n_lang : tempOtherWords[0].f_lang}
      onClick={clickHandler}
      id={tempOtherWords[0].word_id}
      isSuccess={false}
      trueName={isNative ? currentWord.word.n_lang : currentWord.word.f_lang}
    />,
    <Card
      name={isNative ? tempOtherWords[1].n_lang : tempOtherWords[1].f_lang}
      onClick={clickHandler}
      id={tempOtherWords[1].word_id}
      isSuccess={false}
      trueName={isNative ? currentWord.word.n_lang : currentWord.word.f_lang}
    />,
    <Card
      name={isNative ? tempOtherWords[2].n_lang : tempOtherWords[2].f_lang}
      onClick={clickHandler}
      id={tempOtherWords[2].word_id}
      isSuccess={false}
      trueName={isNative ? currentWord.word.n_lang : currentWord.word.f_lang}
    />,
  ];

  shuffle(cards);
  return (
    <CardHolderWrapper>
      <Line>
        {cards[0]}
        {cards[1]}
      </Line>
      <Line>
        {cards[2]}
        {cards[3]}
      </Line>
    </CardHolderWrapper>
  );
};

export default observer(CardHolder);
