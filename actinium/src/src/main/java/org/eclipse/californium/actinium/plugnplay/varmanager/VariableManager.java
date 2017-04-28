package org.eclipse.californium.actinium.plugnplay.varmanager;

import org.eclipse.californium.actinium.Utils;
import org.eclipse.californium.actinium.cfg.Config;
import org.eclipse.californium.actinium.plugnplay.JavaScriptApp;

import javax.script.Bindings;
import javax.script.SimpleBindings;
import java.io.File;
import java.io.FileWriter;
import java.io.IOException;
import java.util.Set;

import static org.eclipse.californium.actinium.AcShell.ERR_FILE_IO;

/**
 * Created by shouxian on 16/1/17.
 */
public class VariableManager {

    Bindings engineScope;
    String filePath;
    JavaScriptApp app;

    public VariableManager(JavaScriptApp app){
        this.app = app;

        engineScope = new SimpleBindings();
    }

    public Bindings getVariableBindings() {
        return engineScope;
    }

    public void addMapping(String key, Object val) {
        if(engineScope.containsKey(key)) {
            engineScope.replace(key, val);
        } else {
            engineScope.put(key, val);
        }
    }

    public void saveToVarConfigFile() {
        Set<String> keys = engineScope.keySet();
        String conf = "";

        for(String key : keys) {
            String val = (String) engineScope.get(key);
            conf += key + " = \'" + val + "\';\n";
        }

        FileWriter appfile = null;
        try {
            String apppath = getVarConfigPath();

            // create new file object
            File f = new File(apppath);

            if(!f.exists())
                f.createNewFile();

            appfile = new FileWriter(f);
            appfile.write(conf);

        } catch (IOException e) {
            e.printStackTrace();
            throw new RuntimeException("Internal error while trying to store var_config to disk. IOException: " + e.getMessage(), e);
        } catch (SecurityException e) {
            e.printStackTrace();
            throw new RuntimeException("Internal error while trying to write to disk. SecurityException: " + e.getMessage(), e);
        } finally {
            try {
                if (appfile != null)
                    appfile.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    /**
     * Returns the path to the file with the var-config of this app.
     *
     * @return the path to the file with the var-config of this app.
     */
    private String getVarConfigPath() {
        return app.getManager().getConfig().getProperty(Config.APP_VAR_CONFIG_PATH) + app.getName() + "_var_config" + app.getManager().getConfig().getProperty(Config.JAVASCRIPT_SUFFIX);
    }

    public boolean varConfigExists() {
        File file = new File(getVarConfigPath());

        File appConfigFolder = new File(app.getManager().getConfig().getProperty(Config.APP_VAR_CONFIG_PATH));
        if(!appConfigFolder.exists()) {
            if (appConfigFolder.mkdir()) {
                throw new RuntimeException("Unable to create directory for varconfigs folder. Directory was not created.");
            }
        }

        return file.exists();
    }

    public String loadFromVarConfigFile() {
        String path = getVarConfigPath();
        File file = new File(path);
        String vars = Utils.readFile(file);

        if (vars==null) {
            System.err.println("File not found: " + path);
            System.exit(ERR_FILE_IO);
        }

        return vars;
    }
}
