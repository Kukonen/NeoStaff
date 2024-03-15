# Generated by Django 4.1.13 on 2024-03-15 10:39

from django.db import migrations, models
import django.db.models.deletion
import djongo.models.fields


class Migration(migrations.Migration):

    initial = True

    dependencies = [
    ]

    operations = [
        migrations.CreateModel(
            name='Activity',
            fields=[
                ('_id', djongo.models.fields.ObjectIdField(auto_created=True, primary_key=True, serialize=False)),
                ('date', models.DateField()),
                ('note', models.TextField()),
                ('mark', models.IntegerField()),
                ('type', models.TextField()),
            ],
        ),
        migrations.CreateModel(
            name='Position',
            fields=[
                ('_id', djongo.models.fields.ObjectIdField(auto_created=True, primary_key=True, serialize=False)),
                ('title', models.TextField()),
                ('requirementScores', models.IntegerField()),
            ],
        ),
        migrations.CreateModel(
            name='Employee',
            fields=[
                ('_id', djongo.models.fields.ObjectIdField(auto_created=True, primary_key=True, serialize=False)),
                ('serviceNumber', models.TextField()),
                ('surname', models.TextField()),
                ('lastname', models.TextField()),
                ('name', models.TextField()),
                ('scores', models.IntegerField()),
                ('activities', djongo.models.fields.ArrayReferenceField(blank=True, null=True, on_delete=django.db.models.deletion.CASCADE, to='application.activity')),
                ('positions', models.ManyToManyField(to='application.position')),
            ],
        ),
    ]