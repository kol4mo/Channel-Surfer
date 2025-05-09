void ToonShading_float(in float3 Normal, in float ToonRampSmoothness, in float3 ClipSpacePos, in float3 WorldPos, in float4 ToonRampTinting,
in float ToonRampOffset, in float toonlayers, out float3 ToonRampOutput, out float3 Direction)
{
 
    // set the shader graph node previews
    #ifdef SHADERGRAPH_PREVIEW
        ToonRampOutput = float3(0.5,0.5,0);
        Direction = float3(0.5,0.5,0);
    #else
 
        // grab the shadow coordinates
        #if SHADOWS_SCREEN
            half4 shadowCoord = ComputeScreenPos(ClipSpacePos);
        #else
            half4 shadowCoord = TransformWorldToShadowCoord(WorldPos);
        #endif 
 
        // grab the main light
        #if _MAIN_LIGHT_SHADOWS_CASCADE || _MAIN_LIGHT_SHADOWS
            Light light = GetMainLight(shadowCoord);
        #else
            Light light = GetMainLight();
        #endif
 
        // dot product for toonramp
        half l = dot(Normal, light.direction) * 0.5 + 0.5;
        half t = 0;
        for (int i = 0; i < 4; i++){
            half s = step( ((.25)*i), l);
            half t = t + s;
        }
        //d = t * .25;///toonlayers;
        half d;
        for (int i = 0; i < toonlayers; i++) {  
            d = d + step( ((1/toonlayers)*i), l);
        }
        d = d * (1/toonlayers);
        // toonramp in a smoothstep
        half toonRamp = smoothstep(ToonRampOffset, ToonRampOffset+ ToonRampSmoothness, d );
        // multiply with shadows;
        //d *= light.shadowAttenuation;
        // add in lights and extra tinting
        ToonRampOutput =  (d + ToonRampTinting);//light.color * (toonRamp + ToonRampTinting) ;
        // output direction for rimlight
        Direction = light.direction;
    #endif
 
}
