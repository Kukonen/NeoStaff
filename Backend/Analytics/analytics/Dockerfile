# Используем базовый образ Python
FROM python:3.11

# Копируем requirements.txt в контейнер
COPY requirements.txt /app/requirements.txt
# Устанавливаем зависимости
RUN pip install --no-cache-dir -r /app/requirements.txt

# Копируем исходный код приложения в образ
COPY . /app

# Устанавливаем рабочую директорию
WORKDIR /app

# Определяем переменные окружения
ENV DJANGO_SETTINGS_MODULE=analytics.settings
ENV MONGO_URI=mongodb://localhost:27017/

# Открываем порт 8000 для доступа к веб-сервису
EXPOSE 8000

# Запускаем Django приложение
CMD ["python", "manage.py", "runserver", "0.0.0.0:8000"]