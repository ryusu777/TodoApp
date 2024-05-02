FROM node:21.7.3-alpine
WORKDIR /App

COPY ./FrontEnd ./
RUN npm install
RUN npm run build

EXPOSE 3000

ENTRYPOINT ["node", ".output/server/index.mjs"]
