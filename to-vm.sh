#!/bin/bash

set -x

rsync -rva /vagrant/workout-tracker/api/ /home/vagrant/repos/workout-tracker-api/
