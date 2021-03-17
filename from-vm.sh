#!/bin/bash

set -x

rsync -rva --del /home/vagrant/repos/workout-tracker-api/ /vagrant/workout-tracker/api/
