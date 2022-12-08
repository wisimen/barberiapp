import 'dart:convert';
import 'package:barberiapp/helpers/constants.dart';
import 'package:barberiapp/helpers/default_loading_widget.dart';
import 'package:barberiapp/models/barberia.dart';
import 'package:flutter/material.dart';
import "package:barberiapp/helpers/colores.dart";
import 'package:http/http.dart' as http;
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:url_launcher/url_launcher.dart';

Widget getBarbershopsList(
    String? token, void Function(Widget) assignationCallback) {
  if (token == null) {
    return const Center(
      child: Text('No hay token'),
    );
  }
  http.get(
    barberiaControllerUri,
    headers: <String, String>{
      "Authorization": "Bearer $token",
      'Content-Type': 'application/json; charset=UTF-8',
      'accept': 'application/json',
    },
  ).then((response) {
    if (response.statusCode == 200) {
      var jsonResponse = jsonDecode(response.body);
      const msjWhatsapp = "Hola buenos dias, me gustaria agendar una cita.";
      // add list inside column and call assignationCallback
      assignationCallback(
        Column(children: <Widget>[
          const Text(
            'Lista de barberías',
            style: TextStyle(
              color: color_fondo,
              fontSize: 32,
              decoration: TextDecoration.none,
            ),
          ),
          //
          for (var barberia
              in jsonResponse.map((e) => Barberia.fromJson(e)).toList())
            Card(
              shadowColor: color_card_shadow,
              color: color_card,
              child: InkWell(
                child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: <Widget>[
                      SizedBox(
                        width: 100,
                        height: 100,
                        child: Padding(
                          padding: const EdgeInsets.symmetric(vertical: 20),
                          child: ClipOval(
                            child: Image.network(
                              barberia.logoFile,
                              fit: BoxFit.fill,
                              loadingBuilder: (context, child, loadingProgress) {
                                if (loadingProgress == null) return child;
                                return defaultLoadingWidget();
                              },
                            ),
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.fromLTRB(0, 20, 0, 10),
                        child: Text(
                          barberia.nombre,
                          style: const TextStyle(
                            color: color_textos_card,
                            fontSize: 32,
                            decoration: TextDecoration.none,
                          ),
                        ),
                      ),
                      Padding(
                        padding: const EdgeInsets.fromLTRB(0, 0, 0, 10),
                        child: Text(
                          barberia.direccion,
                          style: TextStyle(
                            color: color_textos_card.withOpacity(0.8),
                            fontSize: 20,
                            decoration: TextDecoration.none,
                          ),
                        ),
                      ),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: <Widget>[
                          if (!(barberia.urlUbicacion?.isEmpty ?? true))
                            IconButton(
                              icon: const Icon(
                                FontAwesomeIcons.locationDot,
                                color: color_location_icon,
                              ),
                              onPressed: () {
                                launchUrl(
                                  Uri.tryParse(barberia.urlUbicacion ?? '') ??
                                      Uri.parse('https://maps.google.com'),
                                  mode:
                                      LaunchMode.externalNonBrowserApplication,
                                );
                              },
                            ),
                          if (!(barberia.urlInstagram?.isEmpty ?? true))
                            IconButton(
                              icon: const Icon(
                                FontAwesomeIcons.instagram,
                                color: color_instagram_icon,
                              ),
                              onPressed: () {
                                launchUrl(
                                  Uri.tryParse(barberia.urlInstagram!) ??
                                      Uri(host: "https://www.instagram.com"),
                                  mode:
                                      LaunchMode.externalNonBrowserApplication,
                                );
                              },
                            ),
                          if (!(barberia.urlFacebook?.isEmpty ?? true))
                            IconButton(
                              icon: const Icon(
                                FontAwesomeIcons.facebook,
                                color: color_facebook_icon,
                              ),
                              onPressed: () {
                                launchUrl(
                                  Uri.tryParse(barberia.urlFacebook!) ??
                                      Uri(host: "https://www.facebook.com"),
                                  mode:
                                      LaunchMode.externalNonBrowserApplication,
                                );
                              },
                            ),
                          if (!(barberia.celular?.isEmpty ?? true))
                            IconButton(
                              icon: const Icon(FontAwesomeIcons.whatsapp,
                                color: color_whatsapp_icon,),
                              onPressed: () {
                                launchUrl(
                                  Uri(
                                      host:
                                          "https://api.whatsapp.com/send?phone=${barberia.celular}&text=$msjWhatsapp"),
                                  mode:
                                      LaunchMode.externalNonBrowserApplication,
                                );
                              },
                            ),
                          if (!(barberia.urlYoutube?.isEmpty ?? true))
                            IconButton(
                              icon: const Icon(FontAwesomeIcons.youtube,
                                color: color_youtube_icon,),
                              onPressed: () {
                                launchUrl(
                                  Uri.tryParse(barberia.urlYoutube!) ??
                                      Uri(host: "https://www.youtube.com"),
                                  mode:
                                      LaunchMode.externalNonBrowserApplication,
                                );
                              },
                            ),
                        ],
                      )
                    ]),
                onTap: () async {},
              ),
            ),
          //end  for
        ]),
      );
      return;
    }
    assignationCallback(
      const Center(
        child: Text('No hay barberias'),
      ),
    );
  });
  return defaultLoadingWidget();
}


//     Widget builderFunction(BuildContext context, AsyncSnapshot<http.Response> response) {
//       if (response.connectionState != ConnectionState.done || response.data == null) {
//         return const Center(child: CircularProgressIndicator());
//       }
//       List<Barberia> barbershopsList=[];
//       if (response.data?.statusCode == 200) {
//         var jsonResponse = json.decode(response.data?.body??'');
//         for (var item in jsonResponse) {
//           barbershopsList.add(Barberia.fromJson(item));
//         }
//       }
//       const msjWhatsapp = "Hola buenos dias, me gustaria agendar una cita.";
//       return ListWheelScrollView(
//         itemExtent: 100,
//         diameterRatio: 1.2,
//         useMagnifier: true,
//         magnification: 0.5,
//         children: <Widget>[
//           for (var barbershop in barbershopsList)
//             // Container(
//             // //padding vertical of 20
//             // padding: const EdgeInsets.symmetric(vertical: 20),
//             // child: Column(
//             //   // center all children
//             //   mainAxisAlignment: MainAxisAlignment.center,
//             //   children: <Widget>[
//             //     // ClipOval(
//             //     //   child: Image.network(
//             //     //     barbershop.logoFile,
//             //     //     fit: BoxFit.cover,
//             //     //   ),
//             //     // ),
//                 ElevatedButton(
//                   onPressed: null,
//                   child: Text(
//                     barbershop.nombre,
//                     style: const TextStyle(
//                       fontSize: 20,
//                       fontWeight: FontWeight.bold,
//                       color: color_principal
//                     ),
//                   ),
//                 ),
//                   //instagram link
//                   // Row(children: [
//                   //     // icon button with instagram icon and link,
//                   //     // on pressed open instagram link
                      
//                   //     // icon button with facebook icon and link,
//                   //     // on pressed open facebook link
                      
//                   //     // icon button with whatsapp icon and link,
//                   //     // on pressed open whatsapp link
                      
//                   //     // icon button with youtube icon and link,
//                   //     // on pressed open youtube link
                     
//                   //     // icon button with location icon and link,
//                   //     // on pressed open google maps link
//                   //     IconButton(
//                   //       icon: const Icon(FontAwesomeIcons.locationDot),
//                   //       onPressed: () {
//                   //         launchUrl(
//                   //           Uri( host: barbershop.urlUbicacion),
//                   //           mode: LaunchMode.externalNonBrowserApplication,
//                   //         );
//                   //       },
//                   //     ),
//                   //   ]
//                   // ),
//             //     ],
//             //   )
//             // ),
//         ],
//       );
//     }
//   return FutureBuilder(builder: builderFunction, future: request);
// }

// List<Widget> getbarbershopList(List<Barberia>  barbershopsList){
//   const msjWhatsapp = "Hola buenos dias, me gustaria agendar una cita.";
//   return barbershopsList.map((barbershop) {
//           var widget = Container(
//             //padding vertical of 20
//             padding: const EdgeInsets.symmetric(vertical: 20),
//             child: Column(
//               // center all children
//               mainAxisAlignment: MainAxisAlignment.center,
//               children: <Widget>[
//                 ClipOval(
//                   child: Image.network(
//                     barbershop.logoFile,
//                     fit: BoxFit.cover,
//                   ),
//                 ),
//                 Text(
//                   barbershop.nombre,
//                   style: const TextStyle(
//                     fontSize: 20,
//                     fontWeight: FontWeight.bold,
//                     color: color_principal
//                   ),
//                   ),
//                   //instagram link
//                   Row(children: [
//                       // icon button with instagram icon and link,
//                       // on pressed open instagram link
//                       IconButton(
//                         icon: const Icon(FontAwesomeIcons.instagram),
//                         onPressed: () {
//                           launchUrl(
//                             Uri.tryParse(barbershop.urlInstagram!)?? Uri( host: "https://www.instagram.com"),
//                             mode: LaunchMode.externalNonBrowserApplication,
//                           );
//                         },
//                       ),
//                       // icon button with facebook icon and link,
//                       // on pressed open facebook link
//                       IconButton(
//                         icon: const Icon(FontAwesomeIcons.facebook),
//                         onPressed: () {
//                           launchUrl(
//                             Uri.tryParse(barbershop.urlFacebook!)?? Uri( host: "https://www.facebook.com"),
//                             mode: LaunchMode.externalNonBrowserApplication,
//                           );
//                         },
//                       ),
//                       // icon button with whatsapp icon and link,
//                       // on pressed open whatsapp link
//                       IconButton(
//                         icon: const Icon(FontAwesomeIcons.whatsapp),
//                         onPressed: () {
//                           launchUrl(
//                             Uri( host: "https://api.whatsapp.com/send?phone=${barbershop.celular}&text=$msjWhatsapp"),
//                             mode: LaunchMode.externalNonBrowserApplication,
//                           );
//                         },
//                       ),
//                       // icon button with youtube icon and link,
//                       // on pressed open youtube link
//                       IconButton(
//                         icon: const Icon(FontAwesomeIcons.youtube),
//                         onPressed: () {
//                           launchUrl(
//                             Uri.tryParse(barbershop.urlYoutube!)?? Uri( host: "https://www.youtube.com"),
//                             mode: LaunchMode.externalNonBrowserApplication,
//                           );
//                         },
//                       ),
//                       // icon button with location icon and link,
//                       // on pressed open google maps link
//                       IconButton(
//                         icon: const Icon(FontAwesomeIcons.locationDot),
//                         onPressed: () {
//                           launchUrl(
//                             Uri( host: barbershop.urlUbicacion),
//                             mode: LaunchMode.externalNonBrowserApplication,
//                           );
//                         },
//                       ),
//                     ]
//                   ),
//                 ],
//               )
//             );
//             return widget;
//           }
//         ).toList();
// }