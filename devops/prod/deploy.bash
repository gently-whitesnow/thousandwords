# Перекладываем статику
sudo rm -rf /usr/share/nginx/html/*
sudo mkdir -p /usr/share/nginx/html
sudo mv -rf ~/build/* /usr/share/nginx/html
# Перекладываем конфигурационный файл
sudo rm -rf /etc/nginx/*
sudo mkdir -p /etc/nginx
sudo mv -rf ~/nginx.conf /etc/nginx

# Запускаем сервис
docker-compose up -d


# c ci-cd статика, compose file , nginx.conf