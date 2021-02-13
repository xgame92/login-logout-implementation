
## Auth Login implementation

1-) "What if the list of hard-coded credentials had 3M records, instead of 3 or 4?"
Answer : I would recommend to use cron task for that.

## Default User List:
#### UserName = yigit" Pass = D8da1_*

#### UserName = anastasia" &&Pass = D8da1_*

#### UserName = rebeca" && Pass = D8da1_*

#### UserName = john" && Pass = D8da1_*

## Asp.net Core Server Requirements

#### Install Docker:
```bash
https://www.docker.com/
```

#### Install Postgre Sql:
```bash
docker pull postgres
```

#### Run Postgre Sql:
```bash
docker-compose up
```

#### To Dockerize:
```bash
docker build -t aspnetapp .
```

#### More detail:
```bash
https://docs.docker.com/engine/examples/dotnetcore/
```

#### Create Database(It will automatically connect and create the DB as Default):
```bash
dotnet ef database update
```
#### Change appsettings.json for your needed :
```bash
docker build -t aspnetapp .
```
## Vue.js SPA Requirements

#### Install dependencies:

```bash
yarn
```

#### Set environment variables:

```bash
cp .env.example .env
```
#### Running Locally

```bash
yarn serve
```

#### Running in Production

```bash
yarn build
```

---

#### For Deployment

```bash
https://cli.vuejs.org/guide/deployment.html
```

---
