# Building the react application into a static html file
FROM node:18.12 AS build_stage
WORKDIR /app
COPY . .
RUN npm install
RUN npm run build



# Putting the static html file in the nginx folder
FROM nginx:alpine
WORKDIR /usr/share/nginx/html
RUN rm -rf ./*
COPY --from=build_stage /app/dist .
ENTRYPOINT ["nginx", "-g", "daemon off;"]