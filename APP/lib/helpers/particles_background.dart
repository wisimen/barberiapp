import 'package:flutter/material.dart';
import 'package:animated_background/animated_background.dart';
import 'package:barberiapp/helpers/colores.dart';

ParticleOptions particles = const ParticleOptions(
  baseColor: color_particulas,
  spawnOpacity: 0.0,
  opacityChangeRate: 0.25,
  minOpacity: 0.1,
  maxOpacity: 0.4,
  particleCount: 40,
  spawnMaxRadius: 15.0,
  spawnMaxSpeed: 20.0,
  spawnMinSpeed: 1.0,
  spawnMinRadius: 10.0,
);

AnimatedBackground generar_fondo_animado(Widget child, TickerProvider vsync) {
  return AnimatedBackground(
    behaviour:  RandomParticleBehaviour(options: particles),
    vsync: vsync,
    child: Center(
      child:SingleChildScrollView(
        padding: EdgeInsets.all(32),
        child: child
      )
    ),
  );
}