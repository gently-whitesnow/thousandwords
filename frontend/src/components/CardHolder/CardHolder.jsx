import React, { useEffect } from "react";
import { observer } from "mobx-react-lite";
import { CardHolderWrapper, Line } from "./CardHolder.styles";
import { useStore } from "../../store";
import { shuffle } from "../../store/wordsStore";
import Card from "../Card";
import InetInfo from "../InetInfo/InetInfo";

const CardHolder = () => {
  const { wordsStore } = useStore();
  const {
    getWords,
    setWord,
    isNative,
    currentWord,
    tempOtherWords,
    wordsQueue,
    getWordsHandler,
    dontSentWordsArray,
    loading,
    setLoading,
    postWordsHandler,
  } = wordsStore;

  useEffect(() => {
    getWordsHandler();
  }, [getWordsHandler]);

  const clickHandler = (word_id) => {
    if (word_id === currentWord?.word.word_id) {
      currentWord.count++;
      setWord(currentWord);
    } else {
      currentWord.count = 0;
      setWord(currentWord);
    }
    getWords();
  };

  let cards = [];
  if (tempOtherWords?.length === 3) {
    cards = [
      <Card
        name={isNative ? currentWord?.word.n_lang : currentWord?.word.f_lang}
        onClick={clickHandler}
        id={currentWord?.word.word_id}
        isSuccess={true}
        trueName={
          isNative ? currentWord?.word.n_lang : currentWord?.word.f_lang
        }
      />,
      <Card
        name={
          isNative
            ? tempOtherWords[0]?.word.n_lang
            : tempOtherWords[0]?.word.f_lang
        }
        onClick={clickHandler}
        id={tempOtherWords[0]?.word.word_id}
        isSuccess={false}
        trueName={
          isNative ? currentWord?.word.n_lang : currentWord?.word.f_lang
        }
      />,
      <Card
        name={
          isNative
            ? tempOtherWords[1]?.word.n_lang
            : tempOtherWords[1]?.word.f_lang
        }
        onClick={clickHandler}
        id={tempOtherWords[1]?.word.word_id}
        isSuccess={false}
        trueName={
          isNative ? currentWord?.word.n_lang : currentWord?.word.f_lang
        }
      />,
      <Card
        name={
          isNative
            ? tempOtherWords[2]?.word.n_lang
            : tempOtherWords[2]?.word.f_lang
        }
        onClick={clickHandler}
        id={tempOtherWords[2]?.word.word_id}
        isSuccess={false}
        trueName={
          isNative ? currentWord?.word.n_lang : currentWord?.word.f_lang
        }
      />,
    ];
    shuffle(cards);
  }

  return (
    <CardHolderWrapper>
      {wordsQueue?.length <= 3 && wordsQueue?.length > 0 ? (
        <InetInfo
          words={dontSentWordsArray}
          loading={loading}
          setLoading={setLoading}
          trySend={() => postWordsHandler(null, wordsQueue.map((e) => e.word.word_id))}
        />
      ) : (
        <>
          <Line>
            {cards[0] ?? null}
            {cards[1] ?? null}
          </Line>
          <Line>
            {cards[2] ?? null}
            {cards[3] ?? null}
          </Line>
        </>
      )}
    </CardHolderWrapper>
  );
};

export default observer(CardHolder);
