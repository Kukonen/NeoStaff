from kafka import KafkaConsumer

# Настройки Kafka
bootstrap_servers = ['localhost:9092']
topic = 'data-topic'
group_id = 'analytics-group'

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