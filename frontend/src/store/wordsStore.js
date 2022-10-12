import { makeAutoObservable, configure } from "mobx";
import api from "../api";
import mockWords from "../mocks/words.json";

class WordsStore {
  constructor(rootStore) {
    this.rootStore = rootStore;
    makeAutoObservable(this);
    configure({
      enforceActions: "never",
    });
    this.init();
  }

  init = () => {
    this.getWordsHandler(10);
  };

  currentWord = null;
  tempOtherWords = null;
  wordsQueue = [];
  otherWords = [];
  level = 0;
  isNative = true;

  setIsNative = () => {
    this.isNative = !this.isNative;
    this.setWord(this.currentWord);
    this.getWord();
    this.getOtherWords();
  };

  getWord = () => {
    this.currentWord = this.wordsQueue.pop();
  };
  getOtherWords = () => {
    shuffle(this.otherWords);
    this.tempOtherWords = this.otherWords.slice(3);
  };
  setWord = (word) => {
    shuffle(this.wordsQueue);
    this.wordsQueue.unshift(word);
  };

  getWordsHandler = (count) => {
    api
      .getWords(count)
      .then(({ data }) => {
        this.wordsQueue = data.words;
        this.otherWords = data.other_words;
        this.level = data.level;
        if (this.currentWord === null || this.tempOtherWords === null) {
          this.getWord();
          this.getOtherWords();
        }
      })
      .catch((err) => {
        this.wordsQueue = mockWords.words?.map((e) => {
          return { word: e, count: 0 };
        });
        this.otherWords = mockWords.other_words;
        this.level = mockWords.user_level;
        if (this.currentWord === null || this.tempOtherWords === null) {
          this.getWord();
          this.getOtherWords();
        }
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
