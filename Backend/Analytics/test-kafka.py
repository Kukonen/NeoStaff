import os
import time
from kafka import KafkaConsumer

# Получаем значения переменных окружения из docker-compose.yml
bootstrap_servers = [os.environ.get('KAFKA_BOOTSTRAPADDRESS', 'localhost:9092')]
topic = os.environ.get('KAFKA_ANALYTICS_TOPIC', 'analytics-topic')
group_id = os.environ.get('KAFKA_GROUPID', '7655')

print(f"Bootstrap servers: {bootstrap_servers}")

# Добавляем задержку перед созданием KafkaConsumer
time.sleep(45)  # Задержка в 45 секунд

# Создаем Kafka consumer
consumer = KafkaConsumer(topic,
                         group_id=group_id,
                         bootstrap_servers=bootstrap_servers,
                         auto_offset_reset='earliest',
                         enable_auto_commit=True)

# Обрабатываем полученные сообщения
for message in consumer:
    # Здесь можно добавить код для обработки сообщения
    print(f"Received message: {message.value.decode('utf-8')}")