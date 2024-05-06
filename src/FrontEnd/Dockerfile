FROM node:21.7.3-alpine as build-env
WORKDIR /App
COPY ./FrontEnd/package.json .
COPY ./FrontEnd/package-lock.json .


FROM build-env AS dependencies
RUN npm install

FROM dependencies AS release
COPY ./FrontEnd ./
RUN npm run build

EXPOSE 3000

ENTRYPOINT ["node", ".output/server/index.mjs"]
