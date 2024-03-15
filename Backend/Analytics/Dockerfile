# Используем официальный образ Python
FROM python:3.11

# Копируем requirements.txt в контейнер
COPY requirements.txt /app/requirements.txt

# Устанавливаем зависимости
RUN pip install --no-cache-dir -r /app/requirements.txt

# Копируем все файлы вашего приложения в контейнер
COPY . /app

# Указываем рабочую директорию
WORKDIR /app

# Команда для запуска вашего приложения
# CMD ["python", "test-kafka.py"]