import axios from "axios";

const instance = axios.create({
  baseURL: "https://some-domain.com/api/",
  timeout: 1000,
  headers: { "X-Custom-Header": "foobar" },
});

const getQueryString = (
  module1: string,
  module2: string,
  module3: string,
  module4: string,
  module5: string,
  mark1: number,
  mark2: number,
  mark3: number,
  mark4: number,
  mark5: number,
): string =>
  getModuleQueryString(module1, module2, module3, module4, module5) +
  "&" +
  getMarksQueryString(mark1, mark2, mark3, mark4, mark5).substring(1);

const getMarksQueryString = (
  mark1: number,
  mark2: number,
  mark3: number,
  mark4: number,
  mark5: number,
) =>
  `?mark_1=${mark1}&mark_2=${mark2}&mark_3=${mark3}&mark_4=${mark4}&mark_5=${mark5}`;

const getModuleQueryString = (
  module1: string,
  module2: string,
  module3: string,
  module4: string,
  module5: string,
) =>
  `?module_1=${module1}&module_2=${module2}&module_3=${module3}&module_4=${module4}&module_5=${module5}`;
export { getQueryString, getMarksQueryString, getModuleQueryString };
export default instance;
