from rest_framework import viewsets
from .models import *
from .serializers import *


class PositionModelViewSet(viewsets.ModelViewSet):
    queryset = Position.objects.all()
    serializer_class = PositionModelSerializer


class EmployeeModelViewSet(viewsets.ModelViewSet):
    queryset = Employee.objects.all()
    serializer_class = EmployeeModelSerializer


class ActivityModelViewSet(viewsets.ModelViewSet):
    queryset = Activity.objects.all()
    serializer_class = ActivityModelSerializer
