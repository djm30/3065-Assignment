FROM node:18.12
WORKDIR /app

COPY package.json package.json
RUN npm install --omit=dev
ENV PORT=80
COPY ./src /app/

CMD [ "node", "index.js" ]
EXPOSE 80