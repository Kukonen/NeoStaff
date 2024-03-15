from rest_framework import serializers
from .models import *


class PositionModelSerializer(serializers.ModelSerializer):
    class Meta:
        model = Position
        fields = '__all__'


class EmployeeModelSerializer(serializers.ModelSerializer):
    class Meta:
        model = Employee
        fields = '__all__'


class ActivityModelSerializer(serializers.ModelSerializer):
    class Meta:
        model = Activity
        fields = '__all__'
