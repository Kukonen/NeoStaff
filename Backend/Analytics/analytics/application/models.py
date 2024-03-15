from djongo import models


class Position(models.Model):
    _id = models.ObjectIdField()
    title = models.TextField()
    requirementScores = models.IntegerField()

    class Meta:
        app_label = 'application'


class Activity(models.Model):
    _id = models.ObjectIdField()
    date = models.DateField()
    note = models.TextField()
    mark = models.IntegerField()
    type = models.TextField()

    class Meta:
        app_label = 'application'


class Employee(models.Model):
    _id = models.ObjectIdField()
    serviceNumber = models.TextField()
    surname = models.TextField()
    lastname = models.TextField()
    name = models.TextField()
    scores = models.IntegerField()
    positions = models.ManyToManyField(Position)
    activities = models.ArrayReferenceField(
        to=Activity,
        on_delete=models.CASCADE,
        null=True,
        blank=True
    )

    class Meta:
        app_label = 'application'
