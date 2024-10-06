FROM node:20-alpine as build-env
WORKDIR /App
COPY ./FrontEnd/package.json .

FROM build-env AS dependencies
RUN npm install

FROM dependencies AS run
ENV NODE_ENV=development
EXPOSE 3000

ENTRYPOINT ["npm", "run", "dev"]
