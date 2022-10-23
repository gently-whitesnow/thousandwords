import { makeAutoObservable, configure } from "mobx";
import api from "../api";

class WordsStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
  }

  currentWord = null;
  tempOtherWords = null;
  wordsQueue = [];
  level = 0;
  isNative = true;
  dontSentWordsArray = [];

  setIsNative = () => {
    this.isNative = !this.isNative;
    this.setWord(this.currentWord);
    this.getWords();
  };

  getWords = () => {
    console.log(this.wordsQueue.length);
    console.log(this.dontSentWordsArray.length);
    console.log(this.wordsQueue.map((e)=>e.word.n_lang));
    this.currentWord = this.wordsQueue.pop();
    this.tempOtherWords = this.wordsQueue.slice(0, 3);
  };

  setWord = (wordentity) => {
    shuffle(this.wordsQueue);
    if (wordentity.count === 3) {
      this.postWordshandler(
        wordentity.word.word_id,
        this.wordsQueue.map((e) => e.word.word_id)
      );
    } else {
      this.wordsQueue.unshift(wordentity);
    }
  };

  queueLength = 20;
  getWordsHandler = () => {
    api
      .getWords(this.queueLength)
      .then(({ data }) => {
        this.wordsQueue = data.words?.map((e) => {
          return { word: e, count: 0 };
        });
        this.level = data.user_level;
        if (this.currentWord === null || this.tempOtherWords === null) {
          this.getWords();
        }
      })
      .catch((err) => {
        console.error(err);
      });
  };
  postWordshandler = (wordId, queueWords) => {
    api
      .postWords(
        [wordId].concat(this.dontSentWordsArray),
        this.queueLength - this.wordsQueue.length,
        queueWords
      )
      .then(({ data }) => {
        data.words?.forEach((word) => {
          this.setWord({ word: word, count: 0 });
        });
        this.level = data.user_level;
      })
      .catch((err) => {
        this.dontSentWordsArray.push(wordId);
        console.error(err);
      });
  };
}

export default WordsStore;

//алгоритм Фишера-Йетса.
export function shuffle(arr) {
  var j, temp;
  for (var i = arr.length - 1; i > 0; i--) {
    j = Math.floor(Math.random() * (i + 1));
    temp = arr[j];
    arr[j] = arr[i];
    arr[i] = temp;
  }
  return arr;
}
