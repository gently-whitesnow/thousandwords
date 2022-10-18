FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /src

COPY ./*.sln ./*/*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ./${file%.*}/ && mv $file ./${file%.*}/; done

RUN dotnet restore
COPY ./ ./
RUN dotnet publish ./ThousandWords.WebApi/ -o /publish -c Release --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build-env /publish .

# change timezone from ETC to local
RUN apt update && apt install tzdata -y
ENV TZ="Europe/Moscow"

# folder with csv
COPY ./ThousandWords.WebApi/languages/*.csv ./languages/

ENTRYPOINT ["dotnet","ThousandWords.WebApi.dll"]
